
using System.Collections.Generic;
using BayiPuan.Entities.Concrete;

namespace BayiPuan.Business.Abstract
{
    public interface IRelationalPersonService
    {
        List<RelationalPerson> GetAll();
        RelationalPerson GetById(int relationalPersonId);
        List<RelationalPerson> GetByRelationalPerson(int relationalPersonId);
        
        RelationalPerson Add(RelationalPerson relationalPerson);
        void Update(RelationalPerson relationalPerson);
        void Delete(RelationalPerson relationalPerson);

    }
}