using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание_8
{
    class GameBox
    {

        int[,] View = new int[4, 4];
        int[,] Removing = new int[4, 4]; //Массив для реализации одновременного снятия костей
        int[,] FlexBones = new int[4, 4]; //Массив для пемещения костей

        List<int> elements = new List<int>(); //Элементы
        List<int> Koordinate = new List<int>(); //Координаты


       
        public int TotalRemBones { get; set; } = 0;


        public void Reader()
        {
            View = new int[4, 4];
            string line;
            using (StreamReader reader = new StreamReader("Игра.txt"))
            {
                int k = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    View[k,0] = Convert.ToInt32(line[0].ToString());
                    View[k,1] = Convert.ToInt32(line[2].ToString());
                    View[k,2] = Convert.ToInt32(line[4].ToString());
                    View[k,3] = Convert.ToInt32(line[6].ToString());
                    k++;
                }
            }
        }


        public void Poisk()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (View[x,y]!=0)
                    {
                        elements.Add(View[x, y]);
                        string koordinata = x + "" + y;
                        Koordinate.Add(Convert.ToInt32(koordinata));
                    }
                }
            }
        }






        public void Analiz(int[,] Movmap, List<int> Koordinate)
        {
            int RemBones = 0;

            for (int x = 0; x < 4; x++) // Обнуление массива
            {
                for (int y = 0; y < 4; y++)
                {
                    Removing[x, y] = 0;
                }
            }
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Removing[x, y] = Convert.ToInt32(Movmap[x, y]);
                }
            }
          
            int n = 0;

            foreach (var item in elements)
            {              
                int x = Koordinate[n]/10;
                int y = Koordinate[n]%10;

                int CountSide = 0; //Количество костей на стороне
                int GhostCountSide = 0; //Количество потециальных костей на строне


                if (x > 0)//Проверка границы вверху
                {
                    if (Removing[x - 1, y] != 0)//вверх
                    {
                        CountSide++;
                    }
                    else
                    {
                        if (Movmap[x - 1, y]!=0)
                        {
                            GhostCountSide++;
                        }
                    }
                }
                if (y < 3)//Проверка границы вправо
                {
                    if (Removing[x, y + 1] != 0)//вправо
                    {
                        CountSide++;
                    }
                    else
                    {
                        if (Movmap[x, y + 1] != 0)
                        {
                            GhostCountSide++;
                        }
                    }
                }
                if (x < 3)//Проверка границы вниз
                {
                    if (Removing[x + 1, y] != 0)//вниз
                    {
                        CountSide++;
                    }
                    else
                    {
                        if (Movmap[x + 1, y] != 0)
                        {
                            GhostCountSide++;
                        }
                    }
                }
                if (y > 0)//Проверка границы влево
                {
                    if (Removing[x, y - 1] != 0)//влево
                    {
                        CountSide++;
                    }
                    else
                    {
                        if (Movmap[x, y - 1] != 0)
                        {
                            GhostCountSide++;
                        }
                    }
                }



                if ((CountSide == elements[n]) || (GhostCountSide == elements[n]))
                {
                    RemBones++;
                    Removing[x, y] = 0;          
                }
                n++;
            }

            if (TotalRemBones<RemBones) //Сравнение количества выкинутых костей с доски при разных размещениях
            {
                TotalRemBones = RemBones;
            }

        }


        public void MoveBons()
        {


            List<int> flexKoordinate = Koordinate;
            int n = 0;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    FlexBones[x, y] = Convert.ToInt32(View[x, y]);
                }
            }

            foreach (var item in elements)
            {
                int i = Koordinate[n] / 10;
                int j = Koordinate[n] % 10;
                FlexBones[i, j] = 0;

                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (FlexBones[x, y]==0)
                        {                           
                            string koordinata = x + "" + y;
                            flexKoordinate[n] = Convert.ToInt32(koordinata); //Координаты перестроения элемента под номером n
                            FlexBones[x, y] = elements[n];
                            Analiz(FlexBones, flexKoordinate);
                            FlexBones[x, y] = 0;
                            
                        }
                                           
                    }
                }
                FlexBones[i, j] = elements[n];
                flexKoordinate[n] = Convert.ToInt32(i + "" + j);
                n++;
            }
           
            
            
        }







        public void Writer(RichTextBox okno)
        {
            for (int i = 0; i < 4; i++)
            {
                okno.Text += View[i,0].ToString()+" "+View[i,1].ToString()+" "+View[i,2].ToString()+" "+View[i, 3].ToString()+"\n";
            }
        }
        public void Writeruthere(RichTextBox okno)
        {
            for (int i = 0; i < 4; i++)
            {
                okno.Text += Removing[i, 0].ToString() + " " + Removing[i, 1].ToString() + " " + Removing[i, 2].ToString() + " " + Removing[i, 3].ToString() + "\n";
            }
        }
    }
}
