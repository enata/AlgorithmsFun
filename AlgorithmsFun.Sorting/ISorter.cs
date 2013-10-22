namespace AlgorithmsFun.Sorting
{
    public interface ISorter<TSortable>
    {
        void Sort(ref TSortable[] unsorted);
    }
}