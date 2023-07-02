using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace Metro
{
    internal class Metro
    {
        public string[] Allomasok;
        public string vegallomas1;
        public string vegallomas2;

        static public string file = "METRO.DAT";
        static public string[] data = File.ReadAllLines(file);
        static public Metro[] metros = new Metro[db(data)];

        public Metro(string[] allomasok)
        {
            this.Allomasok = allomasok;
            this.vegallomas1 = allomasok[0];
            this.vegallomas2 = allomasok[allomasok.Length - 1];
        }

        static public int db(string[] data)
        {
            int db = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int z;
                if (int.TryParse(data[i], out z))
                {
                    db++;
                }
            }
            return db;
        }
        static public void Beolvas()
        {
            int k = 0;
            int j = 0;
            while (k < data.Length)
            {
                int num = int.Parse(data[k++]);

                string[] tmp = new string[num];

                for (int i = 0; i < num; i++)
                {
                    tmp[i] = data[k++];
                }
                metros[j++] = new Metro(tmp);
            }
        }

        //Csak egy metrón belül
        public string MelyikIrany(string honnan, string hova)
        {
            int honnanInd = Metro.HanyadikIndex(this.Allomasok, honnan);
            int hovaInd = Metro.HanyadikIndex(this.Allomasok, hova);
            if (honnanInd < hovaInd)
            {
                return this.vegallomas2;
            }
            else
            {
                return this.vegallomas1;
            }
        }
        //Csak egy metrón belül
        public int MennyiMegallo(string honnan, string hova)
        {
            int honnanInd = Metro.HanyadikIndex(this.Allomasok, honnan);
            int hovaInd = Metro.HanyadikIndex(this.Allomasok, hova);
            if (hovaInd > honnanInd)
            {
                return hovaInd - honnanInd;
            }
            else
            {
                return honnanInd - hovaInd;
            }
        }

        //STATIKUS METÓDUSOK
        static public int HanyadikIndex(string[] allomasok, string allomas)
        {
            for (int i = 0; i < allomasok.Length; i++)
            {
                if (allomasok[i] == allomas)
                {
                    return i;
                }
            }
            return -1;
        }
        static public bool LetezoAllomas(string allomas)
        {
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                for (int j = 0; j < Metro.metros[i].Allomasok.Length; j++)
                {
                    if (Metro.metros[i].Allomasok[j] == allomas)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static public bool UgyanazAMetro(string allomas1, string allomas2)
        {
            bool a = false;
            bool b = false;
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                for (int j = 0; j < Metro.metros[i].Allomasok.Length; j++)
                {
                    if (metros[i].Allomasok[j] == allomas1)
                    {
                        a = true;//igaz, ha benne van az állomás1
                    }
                    if (metros[i].Allomasok[j] == allomas2)
                    {
                        b = true;//igaz, ha benne van az állomás2
                    }
                }
                if (a && b)
                {
                    return true;//igazat adunk vissza, ha mind a kettő benne volt, ha csak az egyik vagy egy sem, akkor nem
                }
                a = false;
                b = false;
            }
            return false;
        }
        static public bool VanEKozosAllomas(Metro metro1, Metro metro2)
        {
            for (int i = 0; i < metro1.Allomasok.Length; i++)
            {
                for (int j = 0; j < metro2.Allomasok.Length; j++)
                {
                    if (metro1.Allomasok[i] == metro2.Allomasok[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static public string MiAkozosAllomas(Metro metro1, Metro metro2)
        {
            for (int i = 0; i < metro1.Allomasok.Length; i++)
            {
                for (int j = 0; j < metro2.Allomasok.Length; j++)
                {
                    if (metro1.Allomasok[i] == metro2.Allomasok[j])
                    {
                        return metro1.Allomasok[i];
                    }
                }
            }
            return "";
        }

        //ugyanúgy működik, mint az ugyanazametro()
        static public Metro MelyikMetro(string allomas1, string allomas2)
        {
            bool a = false;
            bool b = false;
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                for (int j = 0; j < Metro.metros[i].Allomasok.Length; j++)
                {
                    if (metros[i].Allomasok[j] == allomas1)
                    {
                        a = true;
                    }
                    if (metros[i].Allomasok[j] == allomas2)
                    {
                        b = true;
                    }
                }
                if (a && b)
                {
                    return Metro.metros[i];
                }
                a = false;
                b = false;
            }
            return null;
        }

        static public Metro[] MelyikMetro(string allomas)//tömböt ad vissza, mert több metró is lehet amiben bennevan
        {
            //Megszámoljuk, hogy hány olyan metró van, amiben benne van a keresett állomás
            int db = 0;
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                for (int j = 0; j < Metro.metros[i].Allomasok.Length; j++)
                {
                    if (metros[i].Allomasok[j] == allomas)
                    {
                        db++;
                    }
                }
            }
            Metro[] result = new Metro[db];//Ez lesz az a tömb, amit a végén visszaadunk, egyelőre üres
            int k = 0;//indexelő változó, ezzel indexelünk a result tömbre
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                for (int j = 0; j < Metro.metros[i].Allomasok.Length; j++)
                {
                    if (metros[i].Allomasok[j] == allomas)
                    {
                        result[k++] = Metro.metros[i];//ha találtunk egyező állomást, visszaadjuk az aktuális metrót
                    }
                }
            }
            return result;
        }

        static public Metro KezdoMetro(string honnan, string hova)
        {
            if (Metro.MelyikMetro(honnan).Length != 1)//MelyikMetro() egy paraméteres változata egy tömböt ad vissza, ezért megvizsgáljuk, hogy egyné többet adott-e vissza
            {
                for (int i = 0; i < Metro.MelyikMetro(honnan).Length; i++)//végigmegyünk a kezdő metrókon (biztos, hogy 2 van, mert az if miatt beléptünk ide)
                {
                    for (int j = 0; j < Metro.MelyikMetro(hova).Length; j++)
                    {
                        if (Metro.VanEKozosAllomas(Metro.MelyikMetro(honnan)[i], Metro.MelyikMetro(hova)[j]))
                        {
                            return Metro.MelyikMetro(honnan)[i];//ha van közös állomás, visszaadjuk a kezdő metrót
                        }
                    }
                }
            }
            return Metro.MelyikMetro(honnan)[0];//ez csak akkor fut le, ha csak 1-et adott vissza a MelyikMetro()
        }
        static public Metro CelMetro(string honnan, string hova)
        {
            if (Metro.MelyikMetro(honnan).Length != 1)
            {
                for (int i = 0; i < Metro.MelyikMetro(honnan).Length; i++)
                {
                    for (int j = 0; j < Metro.MelyikMetro(hova).Length; j++)
                    {
                        if (Metro.VanEKozosAllomas(Metro.MelyikMetro(honnan)[i], Metro.MelyikMetro(hova)[j]))
                        {
                            return Metro.MelyikMetro(hova)[j];//ugyanaz, csak a hovának vizsgáljuk a metróját
                        }
                    }
                }
            }
            return Metro.MelyikMetro(hova)[0];
        }

        static public Metro MiAzOsszekotoMetro(Metro metro1, Metro metro2)
        {
            for (int i = 0; i < Metro.metros.Length; i++)
            {
                if (Metro.metros[i] != metro1 && Metro.metros[i] != metro2)//csak akkor vizsgáljuk az adott metrót, ha biztos hogy nem az a kezdő vagy a végső metró
                {
                    if (Metro.VanEKozosAllomas(Metro.metros[i], metro1))//Megvizsgáljuk, hogy van-e közös állomása a kezdőmetróval
                    {
                        if (Metro.VanEKozosAllomas(Metro.metros[i], metro2))//Ha van a kezdővel közös, megvizsgáljuk, hogy van-e közös állomása a végsőmetróval
                        {
                            return Metro.metros[i];//ha megtaláltuk a közös állomásokat, visszaadjuk az összekötő metrót.
                        }
                    }
                }
            }
            return null;//ha a forrásfáljhoz képest helyes adatokat adtunk meg, ez soha nem fog lefutni (szintaktika miatt kötelező)
        }
    }
}
