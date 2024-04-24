using System;
using System.Collections.Generic;

class LabirintoRato
{
    private const int Limit = 15;

    static void MostrarLabirinto(char[,] array, int l, int c)
    {
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Console.Write($" {array[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void CriarLabirinto(char[,] meuLab)
    {
        Random random = new Random();
        for (int i = 0; i < Limit; i++)
        {
            for (int j = 0; j < Limit; j++)
            {
                meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
            }
        }

        for (int i = 0; i < Limit; i++)
        {
            meuLab[0, i] = '*';
            meuLab[Limit - 1, i] = '*';
            meuLab[i, 0] = '*';
            meuLab[i, Limit - 1] = '*';
        }

        int x = random.Next(1, Limit - 1);
        int y = random.Next(1, Limit - 1);
        meuLab[x, y] = 'Q';
    }

    static bool BuscarQueijo(char[,] meuLab, int x, int y)
    {
        Stack<(int, int)> pilha = new Stack<(int, int)>();
        pilha.Push((x, y));

        while (pilha.Count > 0)
        {
            (int linha, int coluna) = pilha.Pop();

            if (meuLab[linha, coluna] == 'Q')
            {
                Console.WriteLine("Queijo encontrado na posição ({0}, {1})!", linha, coluna);
                return true;
            }

            if (meuLab[linha, coluna] != '.' && meuLab[linha, coluna] != 'x')
            {
                continue; // Ignorar posições inválidas ou já visitadas
            }

            if (meuLab[linha, coluna] == '.')
            {
                meuLab[linha, coluna] = 'v'; // Marcando a posição como visitada
            }

            // Movimentos possíveis: direita, baixo, esquerda, cima, diagonais
            int[] movimentosLinha = { 0, 1, 0, -1 };
            int[] movimentosColuna = { 1, 0, -1, 0 };

            bool movimentoValido = false;

            for (int i = 0; i < 4; i++)
            {
                int novaLinha = linha + movimentosLinha[i];
                int novaColuna = coluna + movimentosColuna[i];

                if (novaLinha >= 0 && novaLinha < Limit && novaColuna >= 0 && novaColuna < Limit)
                {
                    if (meuLab[novaLinha, novaColuna] == '.' || meuLab[novaLinha, novaColuna] == 'Q')
                    {
                        pilha.Push((novaLinha, novaColuna));
                        movimentoValido = true;
                    }
                }
            }

            if (!movimentoValido)
            {
                meuLab[linha, coluna] = 'x'; // Marcando como beco
            }

            System.Threading.Thread.Sleep(200);
            Console.Clear();
            MostrarLabirinto(meuLab, Limit, Limit);
        }

        Console.WriteLine("Impossível encontrar o queijo! :(");
        return false;
    }

    static void Main(string[] args)
    {
        char[,] labirinto = new char[Limit, Limit];
        CriarLabirinto(labirinto);
        MostrarLabirinto(labirinto, Limit, Limit);
        BuscarQueijo(labirinto, 1, 1);
        Console.ReadKey();
    }
}
