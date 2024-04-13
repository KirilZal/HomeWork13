namespace Part3
{
    class Kvadrat

    {
        protected IEnumerable<int> OddNumbers(int[]arr)
        {
            foreach (int num in arr)
            {
                if(num%2 != 0) { 
                yield return num*num;}
            }
        }
        public IEnumerable<int> Getnum(int[] arr)
        {
            return OddNumbers(arr);
        }


       
    }
}
