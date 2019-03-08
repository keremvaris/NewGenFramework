/*!
 * Mvc.Grid 4.3.0
 * https://github.com/NonFactors/MVC5.Grid
 *
 * Copyright © NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */
var MvcGrid = (function () {
    function MvcGrid(grid, options) {
        this.columns = [];
        this.element = grid;
        options = options || {};
        this.data = options.data;
        this.name = grid.attr('id') || '';
        this.rowClicked = options.rowClicked;
        this.methods = { reload: this.reload };
        this.reloadEnded = options.reloadEnded;
        this.reloadFailed = options.reloadFailed;
        this.reloadStarted = options.reloadStarted;
        this.requestType = options.requestType || 'get';
        this.prefix = this.name == '' ? '' : this.name + '-';
        this.sourceUrl = options.sourceUrl || grid.data('source-url') || '';
        this.filters = $.extend({
            'Text': new MvcGridTextFilter(),
            'Date': new MvcGridDateFilter(),
            'Number': new MvcGridNumberFilter(),
            'Boolean': new MvcGridBooleanFilter()
        }, options.filters);

        if (this.sourceUrl) {
            var splitIndex = this.sourceUrl.indexOf('?');
            if (splitIndex > -1) {
                this.query = this.sourceUrl.substring(splitIndex + 1);
                this.sourceUrl = this.sourceUrl.substring(0, splitIndex);
            } else {
                this.query = options.query || '';
            }
        } else {
            this.query = window.location.search.replace('?', '');
        }

        if (options.reload || (this.sourceUrl && !options.isLoaded)) {
            this.reload();
            return;
        }

        var headers = grid.find('th');
        for (var i = 0; i < headers.length; i++) {
            var column = this.createColumn($(headers[i]));
            this.bindFilter(column);
            this.bindSort(column);
            this.cleanup(column);

            this.columns.push(column);
        }

        var pager = grid.find('.mvc-grid-pager');
        if (pager.length > 0) {
            this.pager = {
                currentPage: pager.find('li.active').data('page') || 0,
                rowsPerPage: pager.find('.mvc-grid-pager-rows'),
                pages: pager.find('li:not(.disabled)'),
                element: pager
            };
        }

        this.bindPager();
        this.bindGrid();
        this.clean();
    }

    MvcGrid.prototype = {
        createColumn: function (header) {
            var column = {};
            column.header = header;
            column.name = header.data('name') || '';

            if (header.data('filter') == 'True') {
                column.filter = {
                    isMulti: header.data('filter-multi') == 'True',
                    operator: header.data('filter-operator') || '',
                    name: header.data('filter-name') || '',
                    first: {
                        type: header.data('filter-first-type') || '',
                        val: header.data('filter-first-val') || ''
                    },
                    second: {
                        type: header.data('filter-second-type') || '',
                        val: header.data('filter-second-val') || ''
                    }
                };
            }

            if (header.data('sort') == 'True') {
                column.sort = {
                    firstOrder: header.data('sort-first') || '',
                    order: header.data('sort-order') || ''
                }
            }

            return column;
        },
        set: function (options) {
            for (var key in options) {
                if (this.hasOwnProperty(key)) {
                    if (key == 'filters') {
                        this.filters = $.extend(this.filters, options.filters);
                    } else if (key == 'sourceUrl') {
                        if (!options.hasOwnProperty('query')) {
                            this.query = '';
                        }

                        this.sourceUrl = options.sourceUrl;
                    } else {
                        this[key] = options[key];
                    }
                }
            }

            if (options.reload) {
                this.reload();
            }
        },

        bindFilter: function (column) {
            if (column.filter) {
                var grid = this;

                column.header.find('.mvc-grid-filter').on('click.mvcgrid', function () {
                    grid.renderFilter(column);
                });
            }
        },
        bindSort: function (column) {
            if (column.sort) {
                var grid = this;

                column.header.on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-filter').length == 0) {
                        grid.applySort(column);
                        grid.reload();
                    }
                });
            }
        },
        bindPager: function () {
            var grid = this;

            if (grid.pager) {
                grid.pager.rowsPerPage.on('change.mvcgrid', function () {
                    grid.applyPage(grid.pager.currentPage);
                    grid.reload();
                });

                grid.pager.pages.on('click.mvcgrid', function () {
                    var page = $(this).data('page');

                    if (page) {
                        grid.applyPage(page);
                        grid.reload();
                    }
                });
            }
        },
        bindGrid: function () {
            var grid = this;

            grid.element.find('tbody tr:not(.mvc-grid-empty-row)').on('click.mvcgrid', function (e) {
                var cells = $(this).find('td');
                var data = [];

                for (var i = 0; i < grid.columns.length; i++) {
                    var column = grid.columns[i];
                    if (i < cells.length) {
                        data[column.name] = $(cells[i]).text();
                    }
                }

                if (grid.rowClicked) {
                    grid.rowClicked(this, data, e);
                }

                $(this).trigger('rowclick', [data, grid, e]);
            });
        },

        reload: function () {
            var grid = this;

            grid.element.trigger('reloadStarted', [grid]);

            if (grid.reloadStarted) {
                grid.reloadStarted();
            }

            if (grid.sourceUrl) {
                $.ajax({
                    cache: false,
                    data: grid.data,
                    type: grid.requestType,
                    url: grid.sourceUrl + '?' + grid.query
                }).done(function (result) {
                    grid.element.hide();
                    grid.element.after(result);

                    var newGrid = grid.element.next('.mvc-grid').mvcgrid({
                        reloadStarted: grid.reloadStarted,
                        reloadFailed: grid.reloadFailed,
                        reloadEnded: grid.reloadEnded,
                        requestType: grid.requestType,
                        rowClicked: grid.rowClicked,
                        sourceUrl: grid.sourceUrl,
                        filters: grid.filters,
                        query: grid.query,
                        data: grid.data,
                        isLoaded: true
                    }).data('mvc-grid');
                    grid.element.remove();

                    newGrid.element.trigger('reloadEnded', [newGrid]);

                    if (newGrid.reloadEnded) {
                        newGrid.reloadEnded();
                    }
                })
                .fail(function (result) {
                    grid.element.trigger('reloadFailed', [grid, result]);

                    if (grid.reloadFailed) {
                        grid.reloadFailed(result);
                    }
                });
            } else {
                window.location.href = '?' + grid.query;
            }
        },
        renderFilter: function (column) {
            var grid = this;
            var filter = grid.filters[column.filter.name];
            var popup = $('body').children('.mvc-grid-popup');

            $(window).off('resize.mvcgrid');
            $(window).off('click.mvcgrid');

            if (filter) {
                filter.render(grid, popup, column.filter);
                filter.init(grid, column, popup);

                grid.setFilterPosition(column, popup);
                popup.addClass('open');

                $(window).on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-popup').length == 0 &&
                        !target.is('[class*="ui-datepicker"]') && target.parents('[class*="ui-datepicker"]').length == 0) {
                        $(window).off('click.mvcgrid');
                        popup.removeClass('open');
                    }
                });

                $(window).on('resize.mvcgrid', function () {
                    if (popup.hasClass('open')) {
                        popup.removeClass('open');

                        grid.setFilterPosition(column, popup);

                        popup.addClass('open');
                    }
                });
            } else {
                popup.removeClass('open');
            }
        },
        setFilterPosition: function (column, popup) {
            var filter = column.header.find('.mvc-grid-filter');
            var documentWidth = $(document).width();
            var arrow = popup.find('.popup-arrow');
            var filterLeft = filter.offset().left;
            var filterTop = filter.offset().top;
            var filterHeight = filter.height();
            var popupWidth = popup.width();

            var popupTop = filterTop + filterHeight / 2 + 14;
            var popupLeft = filterLeft - 8;
            var arrowLeft = 12;

            if (filterLeft + popupWidth + 5 > documentWidth) {
                popupLeft = documentWidth - popupWidth - 5;
                arrowLeft = filterLeft - popupLeft + 4;
            }

            arrow.css('left', arrowLeft + 'px');
            popup.css('left', popupLeft + 'px');
            popup.css('top', popupTop + 'px');
        },

        cancelFilter: function (column) {
            this.queryRemove(this.prefix + 'Page');
            this.queryRemove(this.prefix + 'Rows');
            this.queryRemoveStartingWith(this.prefix + column.name + '-');
        },
        applyFilter: function (column) {
            this.cancelFilter(column);

            this.queryAdd(this.prefix + column.name + '-' + column.filter.first.type, column.filter.first.val);
            if (column.filter.isMulti) {
                this.queryAdd(this.prefix + column.name + '-Op', column.filter.operator);
                this.queryAdd(this.prefix + column.name + '-' + column.filter.second.type, column.filter.second.val);
            }

            if (this.pager) {
                this.queryAdd(this.prefix + 'Rows', this.pager.rowsPerPage.val());
            }
        },
        applySort: function (column) {
            this.queryRemove(this.prefix + 'Sort');
            this.queryRemove(this.prefix + 'Order');
            this.queryAdd(this.prefix + 'Sort', column.name);
            var order = column.sort.order == 'Asc' ? 'Desc' : 'Asc';
            if (!column.sort.order && column.sort.firstOrder) {
                order = column.sort.firstOrder;
            }

            this.queryAdd(this.prefix + 'Order', order);
        },
        applyPage: function (page) {
            this.queryRemove(this.prefix + 'Page');
            this.queryRemove(this.prefix + 'Rows');

            this.queryAdd(this.prefix + 'Page', page);
            this.queryAdd(this.prefix + 'Rows', this.pager.rowsPerPage.val());
        },

        queryAdd: function (key, value) {
            this.query += (this.query ? '&' : '') + encodeURIComponent(key) + '=' + encodeURIComponent(value);
        },
        queryRemoveStartingWith: function (key) {
            var keyToRemove = encodeURIComponent(key);
            var params = this.query.split('&');
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var paramKey = params[i].split('=')[0];
                if (params[i] && paramKey.indexOf(keyToRemove) != 0) {
                    newParams.push(params[i]);
                }
            }

            this.query = newParams.join('&');
        },
        queryRemove: function (key) {
            var keyToRemove = encodeURIComponent(key);
            var params = this.query.split('&');
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var paramKey = params[i].split('=')[0];
                if (params[i] && paramKey != keyToRemove) {
                    newParams.push(params[i]);
                }
            }

            this.query = newParams.join('&');
        },

        cleanup: function (column) {
            var header = column.header;
            header.removeAttr('data-name');

            header.removeAttr('data-filter');
            header.removeAttr('data-filter-name');
            header.removeAttr('data-filter-multi');
            header.removeAttr('data-filter-operator');
            header.removeAttr('data-filter-first-val');
            header.removeAttr('data-filter-first-type');
            header.removeAttr('data-filter-second-val');
            header.removeAttr('data-filter-second-type');

            header.removeAttr('data-sort');
            header.removeAttr('data-sort-order');
            header.removeAttr('data-sort-first');
        },
        clean: function () {
            this.element.removeAttr('data-source-url');
        }
    };

    return MvcGrid;
})();

var MvcGridTextFilter = (function () {
    function MvcGridTextFilter() {
    }

    MvcGridTextFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Text;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.first.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="StartsWith"' + (filter.first.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.first.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                     '</div>' +
                     (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.second.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.second.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="StartsWith"' + (filter.second.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.second.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                     '</div>' :
                     '') +
                     '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                     '</div>' +
                 '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-input');
            inputs.on('keyup.mvcgrid', function (e) {
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(column);
                grid.reload();
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(column);
                    grid.reload();
                }
            });
        }
    };

    return MvcGridTextFilter;
})();

var MvcGridNumberFilter = (function () {
    function MvcGridNumberFilter() {
    }

    MvcGridNumberFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Number;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.second.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var filter = this;

            var inputs = popup.find('.mvc-grid-input');
            inputs.on('keyup.mvcgrid', function (e) {
                if (filter.isValid(this.value)) {
                    $(this).removeClass('invalid');
                    if (e.which == 13) {
                        popup.find('.mvc-grid-apply').click();
                    }
                } else {
                    $(this).addClass('invalid');
                }
            });

            for (var i = 0; i < inputs.length; i++) {
                if (!filter.isValid(inputs[i].value)) {
                    $(inputs[i]).addClass('invalid');
                }
            }
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(column);
                grid.reload();
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(column);
                    grid.reload();
                }
            });
        },

        isValid: function (value) {
            if (!value) return true;
            var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[.,]?\\d*$');

            return pattern.test(value);
        }
    };

    return MvcGridNumberFilter;
})();

var MvcGridDateFilter = (function () {
    function MvcGridDateFilter() {
    }

    MvcGridDateFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Date;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.first.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="NotEquals"' + (filter.second.type == 'NotEquals' ? ' selected="selected"' : '') + '>' + lang.NotEquals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-input');
            if ($.fn.datepicker) { inputs.datepicker(); }

            inputs.on('change.mvcgrid keyup.mvcgrid', function (e) {
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.type = popup.find('.first-filter .mvc-grid-type').val();
                column.filter.first.val = popup.find('.first-filter .mvc-grid-input').val();
                column.filter.second.type = popup.find('.second-filter .mvc-grid-type').val();
                column.filter.second.val = popup.find('.second-filter .mvc-grid-input').val();

                grid.applyFilter(column);
                grid.reload();
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(column);
                    grid.reload();
                }
            });
        }
    };

    return MvcGridDateFilter;
})();

var MvcGridBooleanFilter = (function () {
    function MvcGridBooleanFilter() {
    }

    MvcGridBooleanFilter.prototype = {
        render: function (grid, popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Boolean;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.first.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.first.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.second.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.second.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var inputs = popup.find('.mvc-grid-boolean-filter li');
            inputs.on('click.mvcgrid', function () {
                $(this).addClass('active').siblings().removeClass('active');
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                column.filter.first.type = 'Equals';
                column.filter.second.type = 'Equals';
                column.filter.operator = popup.find('.mvc-grid-operator').val();
                column.filter.first.val = popup.find('.first-filter li.active').data('value');
                column.filter.second.val = popup.find('.second-filter li.active').data('value');

                grid.applyFilter(column);
                grid.reload();
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');

                if (column.filter.first.type || column.filter.second.type) {
                    grid.cancelFilter(column);
                    grid.reload();
                }
            });
        }
    };

    return MvcGridBooleanFilter;
})();

$.fn.mvcgrid = function (options) {
    var args = arguments;

    if (options === 'instance') {
        var instances = [];

        for (var i = 0; i < this.length; i++) {
            var grid = $(this[i]).closest('.mvc-grid');
            if (!grid.length)
                continue;

            var instance = grid.data('mvc-grid');

            if (!instance) {
                grid.data('mvc-grid', instance = new MvcGrid(grid, options));
            }

            instances.push(instance);
        }

        return this.length <= 1 ? instances[0] : instances;
    }

    return this.each(function () {
        var grid = $(this).closest('.mvc-grid');
        if (!grid.length)
            return;

        var instance = grid.data('mvc-grid');

        if (!instance) {
            if (typeof options == 'string') {
                instance = new MvcGrid(grid);
                instance.methods[options].apply(instance, [].slice.call(args, 1));
            } else {
                instance = new MvcGrid(grid, options);
            }

            $.data(grid[0], 'mvc-grid', instance);
        } else if (typeof options == 'string') {
            instance.methods[options].apply(instance, [].slice.call(args, 1));
        } else if (options) {
            instance.set(options);
        }
    });
};

$.fn.mvcgrid.lang = {
    Text: {
        Contains: 'Contains',
        Equals: 'Equals',
        NotEquals: 'Not equals',
        StartsWith: 'Starts with',
        EndsWith: 'Ends with'
    },
    Number: {
        Equals: 'Equals',
        NotEquals: 'Not equals',
        LessThan: 'Less than',
        GreaterThan: 'Greater than',
        LessThanOrEqual: 'Less than or equal',
        GreaterThanOrEqual: 'Greater than or equal'
    },
    Date: {
        Equals: 'Equals',
        NotEquals: 'Not equals',
        LessThan: 'Is before',
        GreaterThan: 'Is after',
        LessThanOrEqual: 'Is before or equal',
        GreaterThanOrEqual: 'Is after or equal'
    },
    Boolean: {
        Yes: 'Yes',
        No: 'No'
    },
    Filter: {
        Apply: '&#10004;',
        Remove: '&#10008;'
    },
    Operator: {
        Select: '',
        And: 'and',
        Or: 'or'
    }
};
$(function () {
    $('body').append('<div class="mvc-grid-popup"></div>');
});
