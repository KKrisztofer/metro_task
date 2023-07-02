using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Metro.Beolvas();

            Console.Write("Add meg az induló állomást: ");
            string honnan = Console.ReadLine();
            Console.Write("Add meg a cél állomást: ");
            string hova = Console.ReadLine();

            Console.Clear();

            if (Metro.LetezoAllomas(honnan) && Metro.LetezoAllomas(hova))
            {
                if (honnan != hova)
                {
                    if (Metro.UgyanazAMetro(honnan, hova))
                    {
                        Console.WriteLine($"Honnan: {honnan}\nHova: {hova}\nÚtvonal:");
                        Console.WriteLine(
                            $"{Metro.MelyikMetro(honnan, hova).MelyikIrany(honnan, hova)}" +
                            $" irányba menj " +
                            $"{Metro.MelyikMetro(honnan, hova).MennyiMegallo(honnan, hova)}" +
                            $" megállót."
                        );
                    }
                    else
                    {
                        if (Metro.VanEKozosAllomas(Metro.KezdoMetro(honnan, hova), Metro.CelMetro(honnan, hova)))
                        {
                            Metro kezdoMetro = Metro.KezdoMetro(honnan, hova);
                            Metro celMetro = Metro.CelMetro(honnan, hova);
                            string atszalloAllomas = Metro.MiAkozosAllomas(kezdoMetro, celMetro);

                            Console.WriteLine($"Honnan: {honnan}\nHova: {hova}\nÚtvonal:");
                            Console.WriteLine(
                                $"{kezdoMetro.MelyikIrany(honnan, atszalloAllomas)}" +// A melyikIrany()-t csak egy metrón belüli állomásokra lehet meghívni!!!! ezért van az átszállóállomás megadva
                                $" irányba menj " +
                                $"{kezdoMetro.MennyiMegallo(honnan, atszalloAllomas)}" +
                                $" megállót, szállj át, " +
                                $"{celMetro.MelyikIrany(atszalloAllomas, hova)}" +
                                $" irányba menj " +
                                $"{celMetro.MennyiMegallo(atszalloAllomas, hova)}" +
                                $" megállót."
                            );
                        }
                        else
                        {
                            Metro kezdoMetro = Metro.KezdoMetro(honnan, hova);
                            Metro celMetro = Metro.CelMetro(honnan, hova);
                            Metro kozepsoMetro = Metro.MiAzOsszekotoMetro(kezdoMetro, celMetro);
                            string atszalloAllomas1 = Metro.MiAkozosAllomas(kezdoMetro, kozepsoMetro);
                            string atszalloAllomas2 = Metro.MiAkozosAllomas(kozepsoMetro, celMetro);

                            Console.WriteLine($"Honnan: {honnan}\nHova: {hova}\nÚtvonal:");
                            Console.WriteLine(
                                $"{kezdoMetro.MelyikIrany(honnan, atszalloAllomas1)}" +
                                $" irányba menj " +
                                $"{kezdoMetro.MennyiMegallo(honnan, atszalloAllomas1)}" +
                                $" megállót, szállj át, " +
                                $"{kozepsoMetro.MelyikIrany(atszalloAllomas1, atszalloAllomas2)}" +
                                $" irányba menj " +
                                $"{kozepsoMetro.MennyiMegallo(atszalloAllomas1, atszalloAllomas2)}" +
                                $" megállót, szállj át, " +
                                $"{celMetro.MelyikIrany(atszalloAllomas2, hova)}" +
                                $" irányba menj " +
                                $"{celMetro.MennyiMegallo(atszalloAllomas2, hova)}" +
                                $" megállót."
                            );
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ugyanazt az állomást adtad meg.");
                }
            }
            else
            {
                Console.WriteLine("Hibás adatot adtál meg!");
            }
        }
    }
}
