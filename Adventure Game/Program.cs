using Adventure_Game.Personagens;
using System;
using System.Collections.Generic;

namespace Adventure_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Personagem jogador = new Personagem(500, 100, 100, 300);
            List<string> menuInicial = new List<string> { "1. Visitar loja - Comprar itens para auxiliar a aventura.",
            "2. Dormir - Recupera todos os pontos de vida e pontos de magia",
            "3. Explorar masmorra - Possibilita entrar na masmorra e enfrentar monstros para evoluir seu personagem e conseguir dinheiro."};
            while (true)
            {
                Console.WriteLine("===============================================================================================================");
                foreach (string menu in menuInicial)
                    Console.WriteLine(menu);
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 3)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 1)
                    Visitar_loja(jogador);
                else if (input == 2)
                    Dormir(jogador);
                else
                    Explorar_masmorra(jogador);
            }
        }
        static void Dormir(Personagem jogador)
        {
            Console.WriteLine("===============================================================================================================");
            Console.WriteLine("Recepcionista: Bem-vindo a nossa taverna aventureiro. Tenha ótimos sonhos");
            Console.WriteLine("Dormindo");
            jogador.PontosDeVida = jogador.PontosDeVidaMax;
            jogador.PontosDeMagia = jogador.PontosDeMagiaMax;
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Recepcionista: Boas Aventuras");
        }
        static void Visitar_loja(Personagem jogador)
        {
            List<string> menuLoja = new List<string> { "1. Poção - Recupera 500 pontos de vida - Preço 100 moedas de ouro",
            "2. Elixir - Recupera 50 pontos de magia - Preço 100 moedas de ouro",
            "3. Voltar ao Menu Inicial"};

            while (true)
            {
                Console.WriteLine("===============================================================================================================");
                Console.WriteLine("Olá, estranho! O que você está comprando?");
                foreach (string menu in menuLoja)
                    Console.WriteLine(menu);
                Console.WriteLine($"Total de Moedas: {jogador.Ouro}");
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 3)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 3)
                    break;
                else
                {
                    Console.WriteLine("Por favor, informe uma quantidade desejada.");
                    int qtd = Check_input(Console.ReadLine());
                    if (qtd <= 0)
                        Console.WriteLine("Por favor, informe uma quantidade positiva.");
                    else
                    {
                        if (jogador.Ouro >= 100 * qtd)
                        {
                            jogador.Ouro = -100 * qtd;
                            if (input == 1)
                                jogador.Pocao = qtd;
                            else
                                jogador.Elixir = qtd;
                        }
                        else
                            Console.WriteLine("Você não possui ouro suficiente para efetuar a compra.");
                    }
                }
            }
        }
        static void Explorar_masmorra(Personagem jogador)
        {
            List<string> menuMasmorra = new List<string> { "1. Entrar em uma sala de monstro",
            "2. Entrar na sala do chefe",
            "3. Voltar ao Menu Inicial"};
            while (true)
            {
                Console.WriteLine("===============================================================================================================");
                Console.WriteLine("Escolha uma das opções abaixo: ");
                foreach (string menu in menuMasmorra)
                    Console.WriteLine(menu);
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 3)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 3)
                    break;
                else
                {
                    if (input == 1)
                        SalaDoMonstro(jogador);
                    else
                        SalaDoChefe(jogador);
                }
            }
        }
        static void SalaDoChefe(Personagem jogador)
        {
            Random random = new Random();
            Personagem chefe = new Personagem(5000, 0, 250, random.Next(500, 1000));
            List<string> menuCombate = new List<string> { "1. Atacar",
            "2. Disparar energia",
            "3. Usar item"};
            int qtdTurnos = 1;
            while (true)
            {
                Console.WriteLine("===============================================================================================================");
                Console.WriteLine("O que você deseja fazer?");
                foreach (string menu in menuCombate)
                    Console.WriteLine(menu);
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 3)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 1)
                    Atacar(jogador, chefe);
                else if (input == 2)
                {
                    if (Disparar_energia(jogador, chefe) == -1)
                        continue;
                }
                else if (input == 3)
                {
                    if (jogador.Pocao == 0 && jogador.Elixir == 0)
                    {
                        Console.WriteLine("Você não possui itens para utilizar");
                        continue;
                    }
                    Usar_item(jogador);
                }
                if (chefe.PontosDeVida > 0)
                    Atacar(chefe, jogador);
                Console.WriteLine("Jogador");
                Console.WriteLine($"Pontos de Vida: {jogador.PontosDeVida} / {jogador.PontosDeVidaMax}");
                Console.WriteLine($"Pontos de Magia: {jogador.PontosDeMagia} / {jogador.PontosDeMagiaMax}");
                Console.WriteLine();
                Console.WriteLine("Monstro");
                Console.WriteLine($"Pontos de Vida: {chefe.PontosDeVida} / {chefe.PontosDeVidaMax}");
                if (chefe.PontosDeVida <= 0)
                {
                    Console.WriteLine($"Parabéns jogador você derrotou o monstro em {qtdTurnos} turnos");
                    jogador.PontosDeVidaMax = 10;
                    jogador.Ouro += chefe.Ouro;
                    Console.WriteLine($"Vida máxima aumentada para {jogador.PontosDeVidaMax}");
                    Console.WriteLine($"{chefe.Ouro} moedas de ouro recebidas");
                    Console.WriteLine("Retornando à sala anterior");
                    Explorar_masmorra(jogador);
                }
                else if (jogador.PontosDeVida <= 0)
                {
                    Console.WriteLine("Monstro: HAHAHA! Fraco, muito fraco! Mande mais aventureiros para me entreter mais");
                    Console.WriteLine("O jogador foi derrotado e foi encaminhado para a taverna na cidade");
                    Dormir(jogador);
                    return;
                }
                qtdTurnos++;
            }
        }

        static void SalaDoMonstro(Personagem jogador)
        {
            Random random = new Random();
            Personagem monstro = new Personagem(500, 0, 100, random.Next(50, 100));
            List<string> menuCombate = new List<string> { "1. Atacar",
            "2. Disparar energia",
            "3. Usar item"};
            int qtdTurnos = 1;
            while (true)
            {
                Console.WriteLine("===============================================================================================================");
                Console.WriteLine("O que você deseja fazer?");
                foreach (string menu in menuCombate)
                    Console.WriteLine(menu);
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 3)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 1)
                    Atacar(jogador, monstro);
                else if (input == 2)
                {
                    if (Disparar_energia(jogador, monstro) == -1)
                        continue;
                }
                else if (input == 3)
                {
                    if (jogador.Pocao == 0 && jogador.Elixir == 0)
                    {
                        Console.WriteLine("Você não possui itens para utilizar");
                        continue;
                    }
                    else
                        Usar_item(jogador);
                }
                if (monstro.PontosDeVida > 0)
                    Atacar(monstro, jogador);
                Console.WriteLine("Jogador");
                Console.WriteLine($"Pontos de Vida: {jogador.PontosDeVida} / {jogador.PontosDeVidaMax}");
                Console.WriteLine($"Pontos de Magia: {jogador.PontosDeMagia} / {jogador.PontosDeMagiaMax}");
                Console.WriteLine();
                Console.WriteLine("Monstro");
                Console.WriteLine($"Pontos de Vida: {monstro.PontosDeVida} / {monstro.PontosDeVidaMax}");
                if (monstro.PontosDeVida <= 0)
                {
                    Console.WriteLine($"Parabéns jogador você derrotou o monstro em {qtdTurnos} turnos");
                    jogador.PontosDeVidaMax = 10;
                    jogador.Ouro = monstro.Ouro;
                    Console.WriteLine($"Vida máxima aumentada para {jogador.PontosDeVidaMax}");
                    Console.WriteLine($"{monstro.Ouro} moedas de ouro recebidas");
                    Console.WriteLine("Retornando à sala anterior");
                    break;
                }
                else if (jogador.PontosDeVida <= 0)
                {
                    Console.WriteLine("Monstro: HAHAHA! Fraco, muito fraco! Mande mais aventureiros para me entreter mais");
                    Console.WriteLine("O jogador foi derrotado e foi encaminhado para a taverna na cidade");
                    Dormir(jogador);
                    break;
                }
                qtdTurnos++;
            }
        }
        static void Atacar(Personagem atacante, Personagem defensor)
        {
            defensor.PontosDeVida = atacante.Ataque * (-1);
        }
        static int Disparar_energia(Personagem atacante, Personagem defensor)
        {
            if (atacante.PontosDeMagia < 50)
                return -1;
            defensor.PontosDeVida = atacante.Ataque * (-2);
            atacante.PontosDeMagia = -50;
            return 0;
        }

        static void Usar_item(Personagem jogador)
        {
            List<string> menuCombate = new List<string> { $"1. Poção ({jogador.Pocao})",
            $"2. Poção ({jogador.Elixir})" };
            while (true)
            {
                if (jogador.Pocao == 0 && jogador.Elixir == 0)
                {
                    Console.WriteLine("Você não possui itens para utilizar");
                    break;
                }
                Console.WriteLine("===============================================================================================================");
                Console.WriteLine("O que você deseja utilizar?");
                foreach (string menu in menuCombate)
                    Console.WriteLine(menu);
                int input = Check_input(Console.ReadLine());
                if (input == -1 || input < 1 || input > 2)
                {
                    Console.WriteLine("Por favor, informe uma opção válida.");
                    continue;
                }
                else if (input == 1)
                {
                    jogador.Pocao = -1;
                    break;
                }
                else if (input == 2)
                {
                    jogador.Elixir = -1;
                    break;
                }
            }
        }


        static int Check_input(string value)
        {
            try
            {
                int result = Convert.ToInt32(value);
                return result;
            }
            catch (OverflowException)
            {
                return -1;
            }
            catch (FormatException)
            {
                return -1;
            }
        }
    }
}


