using Infrastructure.TorreHanoi.ImagemHelper.Dto;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Infrastructure.TorreHanoi.ImagemHelper
{
    public class DesignerService : IDesignerService
    {
        private TorreHanoiDto _torre;

        private int _distanciaEntreTorres;
        private int _alturaMinima;
        private int _larguraTorre;
        private int _alturaTorre;
        private int _larguraDisco;
        private int _larguraPadrao;
        private int _alturaPadrao;
        private int _alturaImagem;
        private int _larguraImagem;
        private Brush _corDiscoPar;
        private Brush _corDiscoImpar;
        private Color _corFundo;


        public void Inicializar(TorreHanoiDto torre)
        {
            _torre = torre;

            _larguraTorre = 10;
            _alturaTorre = 600;
            _larguraDisco = 20;
            _larguraPadrao = 40;
            _alturaPadrao = 50;
            _larguraImagem = 900;
            _alturaImagem = 600;
            _distanciaEntreTorres = 225;
            _alturaMinima = torre.Discos.Count * _alturaPadrao;

            _corDiscoPar = Brushes.Red;
            _corDiscoImpar = Brushes.Blue;
            _corFundo = Color.White;
        }

        public Bitmap Desenhar()
        {
            var imagem = new Bitmap(_larguraImagem, _alturaImagem);
            var graphics = Graphics.FromImage(imagem);
            graphics.Clear(_corFundo);

            graphics = DesenharPinos(graphics);

            graphics = DesenharDiscos(graphics, _torre.Origem);
            graphics = DesenharDiscos(graphics, _torre.Intermediario);
            graphics = DesenharDiscos(graphics, _torre.Destino);

            graphics.Flush();

            return imagem;
        }

        private Graphics DesenharPinos(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, _distanciaEntreTorres * _torre.Origem.Tipo, 0, _larguraTorre, _alturaTorre);
            graphics.FillRectangle(Brushes.Black, _distanciaEntreTorres * _torre.Intermediario.Tipo, 0, _larguraTorre, _alturaTorre);
            graphics.FillRectangle(Brushes.Black, _distanciaEntreTorres * _torre.Destino.Tipo, 0, _larguraTorre, _alturaTorre);

            return graphics;
        }

        private Graphics DesenharDiscos(Graphics graphics, PinoDto pino)
        {
            var discosJaAdicionados = new List<DiscoDto>();

            foreach (var disco in pino.Discos.ToList().OrderByDescending(d => d.Id))
            {
                var alturaDisco = _alturaMinima + (_torre.Discos.Count - discosJaAdicionados.Count) * _larguraDisco;
                var larguraDoDisco = disco.Id * _larguraPadrao;
                var distanciaPosicaoDisco = _distanciaEntreTorres * pino.Tipo - (larguraDoDisco - _larguraTorre) / 2;

                graphics.FillRectangle(disco.Id % 2 == 0 ? _corDiscoPar : _corDiscoImpar, distanciaPosicaoDisco, alturaDisco, larguraDoDisco, _larguraDisco);

                discosJaAdicionados.Add(disco);
            }

            return graphics;
        }
    }
}
