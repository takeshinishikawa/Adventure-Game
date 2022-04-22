using System;

namespace Adventure_Game.Personagens
{
    public class Personagem
    {
        private int _pontosDeVida;
        private int _pontosDeVidaMax;
        private int _pontosDeMagia;
        private int _pontosDeMagiaMax;
        private int _ataque;
        private int _ouro;
        private int _pocao;
        private int _elixir;
        public Personagem(int pontosDeVida, int pontosDeMagia, int ataque, int ouro)
        {
            _pontosDeVidaMax = pontosDeVida;
            _pontosDeVida = _pontosDeVidaMax;
            _pontosDeMagiaMax = pontosDeMagia;
            _pontosDeMagia = _pontosDeMagiaMax;
            _ataque = ataque;
            _ouro = ouro;
            _pocao = 0;
            _elixir = 0;
        }
        public int PontosDeMagiaMax
        {
            get
            {
                return _pontosDeMagiaMax;
            }
        }
        public int PontosDeVidaMax
        {
            get
            {
                return _pontosDeVidaMax;
            }
            set
            {
                _pontosDeVidaMax += value;
            }
        }
        public int PontosDeVida
        { 
            get
            {
                return _pontosDeVida;
            }
            set
            {
                if (value > 0)
                {
                    if (value >= _pontosDeVidaMax)
                        _pontosDeVida = PontosDeVidaMax;
                    else
                        _pontosDeVida += value;
                    return;
                }
                else
                {
                    if (value * (-1) > _pontosDeVida)
                        _pontosDeVida = 0;
                    else
                        _pontosDeVida += value;
                }
            }
        }
        public int PontosDeMagia
        {
            get
            {
                return _pontosDeMagia;
            }
            set
            {
                if (value >= 0)
                {
                    if (_pontosDeMagia + value >= PontosDeMagiaMax)
                        _pontosDeMagia = PontosDeMagiaMax;
                    else
                        _pontosDeMagia += value;
                    return;
                }
                else
                {
                    _pontosDeMagia += value;
                }
            }
        }
        public int Ataque
        {
            get
            {
                return _ataque;
            }
            set
            {
                if (value >= _ataque)
                {
                    _ataque = 0;
                    return;
                }
                else
                {
                    _ataque -= value;
                }
            }
        }
        public int Ouro
        {
            get
            {
                return _ouro;
            }
            set
            {
                if (value >= 0)
                    _ouro += value;
                else
                {
                    if (_ouro < value * (-1))
                    {
                        Console.WriteLine("Não existe ouro suficiente.");
                    }
                    else
                        _ouro += value;
                }
            }
        }
        public int Pocao
        {
            get
            {
                return _pocao;
            }
            set
            {
                if (value >= 0)
                {
                    _pocao += value;
                    return;
                }
                else
                {
                    _pocao += value;
                    if (_pontosDeVida + value * 500 * -1 > _pontosDeVidaMax)
                        _pontosDeVida = _pontosDeVidaMax;
                    else
                        _pontosDeVida -= value * 500;
                }
            }
        }
        public int Elixir
        {
            get
            {
                return _elixir;
            }
            set
            {
                if (value >= 0)
                {
                    _elixir += value;
                    return;
                }
                else
                {
                    _elixir += value;
                    if (_pontosDeMagia + value * 50 * -1 > _pontosDeMagiaMax)
                        _pontosDeMagia = _pontosDeMagiaMax;
                    else
                        _pontosDeMagia -= value * 50;
                }
            }
        }
    }
}
