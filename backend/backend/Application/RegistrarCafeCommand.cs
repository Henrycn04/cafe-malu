using backend.Domain;
using backend.Infrastructure;

namespace backend.Application
{
    public class RegistrarCafeCommand
    {
        private IRegistrarCafe _IRegistrarCafe;

        public RegistrarCafeCommand(IRegistrarCafe IRegistrarCafe)
        {
            this._IRegistrarCafe = IRegistrarCafe;
        }

        public int RegistrarCafe(CafeModel cafe)
        {
            int filas = 0;
            filas = this._IRegistrarCafe.RegistrarCafe(cafe);
            return filas;
        }
    }
}
