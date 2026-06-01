using System;
using System.Threading.Tasks;

namespace Madu_Uss
{
    public static class Heliefektid
    {
        public static void MängiSöömist()
        {
            Task.Run(() => Console.Beep(800, 100));
        }

        public static void MängiKaotust()
        {
            Task.Run(() => {
                Console.Beep(400, 200);
                Console.Beep(200, 300);
            });
        }
    }
}
