using University.BL.Models;


namespace University.BL.Repositories.Implements
{
    public class OfficeRepository : GenericRepository<OfficeAssignment>, IOfficeRepository1
    {
        public OfficeRepository(UniversityEntities context) : base(context)
        {

        }
    }
}
