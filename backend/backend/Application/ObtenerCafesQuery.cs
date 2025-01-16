using backend.Infrastructure;
using backend.Domain;

namespace backend.Application
{
    public class ObtenerCafesQuery
    {
        private IObtenerCafe _IObtenerCafe;

        public ObtenerCafesQuery(IObtenerCafe IObtenerCafe)
        {
            this._IObtenerCafe = IObtenerCafe;
        }

        public ListaCafesModel ObtenerCafes()
        {
            ListaCafesModel cafes = new ListaCafesModel();
            cafes = this._IObtenerCafe.ObtenerCafes();
            return cafes;
        }
    }
}
