using Entity.Model.Security;


namespace Data.interfaces
{
    public interface IViewData
    {
        Task Delete(int id);
        Task<View> GetById(int id);
        Task<View> Save(View entity);
        Task Update(View entity);
        Task<IEnumerable<View>> GetAll();

        //Task<IEnumerable<DataSelectDto>> GetAllSelect();

        Task<View> GetByName(string name);

    }
}
