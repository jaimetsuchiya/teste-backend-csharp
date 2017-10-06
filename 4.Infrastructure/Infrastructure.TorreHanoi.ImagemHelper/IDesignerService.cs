using System.Drawing;
using Infrastructure.TorreHanoi.ImagemHelper.Dto;

namespace Infrastructure.TorreHanoi.ImagemHelper
{
    public interface IDesignerService
    {
        Bitmap Desenhar();
        void Inicializar(TorreHanoiDto torre);
    }
}