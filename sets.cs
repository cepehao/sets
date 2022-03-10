using System;
using System.IO;

namespace _9pLab3._1
{

    class OutsideTheSetException : Exception
    {
        public OutsideTheSetException(string str) : base(str) {

        }
    }

    abstract class Set
    {
        protected int maxSize;

        abstract public void Insert(int x);
        abstract public void Remove(int x);
        abstract public bool Find(int x);

        public void MakeSet(string str) {
            string[] mas = str.Split(' ', ',');
            for (int i = 0; i < mas.Length; i++) {
                Insert(Convert.ToInt32(mas[i]));
            }
        }

        public void MakeSet(int[] mas) {
            for (int i = 0; i < mas.Length; i++) {
                Insert(mas[i]);
            }
        }

        public void Print() {
            bool flag = false;
            for (int i = 1; i <= maxSize; i++) {
                if (Find(i)) {
                    Console.Write($"{i} ");
                    flag = true;
                }
            }
            if (!flag) Console.Write("Пустое множество");
            Console.WriteLine();
        }

        protected static T Intersection<T>(T min, T max, int n) where T : Set {

            T newSet = Activator.CreateInstance(typeof(T), new object[] { n }) as T;

            for (int i = 1; i <= n; i++) {
                if (max.Find(i) && min.Find(i))
                    newSet.Insert(i);
                else {
                    newSet.Remove(i);
                }
            }

            return newSet;
        }

        protected static T Union<T>(T max, T min, int n) where T : Set {
            int nMax = max.maxSize;

            T newSet = Activator.CreateInstance(typeof(T), new object[] { nMax }) as T;

            for (int i = 1; i <= nMax; i++) {
                if (max.Find(i)) {
                    newSet.Insert(i);
                }
            }

            for (int i = 1; i <= n; i++) {
                if (max.Find(i) || min.Find(i))
                    newSet.Insert(i);
            }

            return newSet;
        }
    }

    class SimpleSet : Set
    {
        private bool[] mas;

        public SimpleSet(int maxSize) {
            this.maxSize = maxSize;
            mas = new bool[maxSize];
        }

        override public void Insert(int x) {
            if (x <= 0 || x > maxSize) {
                throw new OutsideTheSetException($"Не удалось вставить элемент со значением {x}");
            }
            else {
                mas[x - 1] = true;
            }
        }

        override public void Remove(int x) {
            mas[x - 1] = false;
        }

        override public bool Find(int x) {
            if (mas[x - 1] == true) {
                return true;
            }
            else {
                return false;
            }
        }

        public static SimpleSet operator +(SimpleSet first, SimpleSet second) {
            int n = Math.Min(first.maxSize, second.maxSize);
            if (n < first.maxSize || first.maxSize == second.maxSize) {
                return Union(first, second, n);
            }
            else {
                return Union(second, first, n);
            }

        }

        public static SimpleSet operator *(SimpleSet first, SimpleSet second) {
            int n = Math.Min(first.maxSize, second.maxSize);
            if (n < first.maxSize || first.maxSize == second.maxSize) {
                return Intersection(first, second, n);
            }
            else {
                return Intersection(second, first, n);
            }
        }

    }

    class BitSet : Set
    {
        private const int INT = 32;
        private int[] mas;

        public BitSet(int maxSize) {
            this.maxSize = maxSize;
            mas = new int[maxSize / INT + 1];
        }

        override public void Insert(int x) {
            if (x <= 0 || x > maxSize) {
                throw new OutsideTheSetException($"Не удалось вставить элемент со значением {x}");
            }
            else {
                int i = x / 32;
                mas[i] = mas[i] | (1 << x % INT - 1);
            }
        }

        override public void Remove(int x) {
            int i = x / 32;
            mas[i] = mas[i] & ~(1 << x % INT - 1);
        }

        override public bool Find(int x) {
            int i = x / 32;
            return (mas[i] & (1 << x % INT - 1)) != 0;
        }

        public static BitSet operator +(BitSet first, BitSet second) {
            int n = Math.Min(first.maxSize, second.maxSize);
            if (n < first.maxSize || first.maxSize == second.maxSize) {
                return Union(first, second, n);
            }
            else {
                return Union(second, first, n);
            }
        }

        public static BitSet operator *(BitSet first, BitSet second) {
            int n = Math.Min(first.maxSize, second.maxSize);
            if (n < first.maxSize || first.maxSize == second.maxSize) {
                return Intersection(first, second, n);
            }
            else {
                return Intersection(second, first, n);
            }
        }
    }

    class MultiSet : Set
    {
        private int[] mas;

        public MultiSet(int maxSize) {
            this.maxSize = maxSize;
            mas = new int[maxSize];
        }

        override public void Insert(int x) {
            if (x <= 0 || x > maxSize) {
                throw new OutsideTheSetException($"Не удалось вставить элемент со значением {x}");
            }
            else {
                mas[x - 1]++;
            }
        }

        override public void Remove(int x) {
            mas[x - 1]--;
        }

        override public bool Find(int x) {
            if (mas[x - 1] > 0) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args) {

        }
    }
}


