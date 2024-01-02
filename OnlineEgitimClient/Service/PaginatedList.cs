namespace OnlineEgitimClient.Service
{
    // Sayfalama işlemleri için kullanılan genel bir sınıf.
    // T türündeki öğeler için sayfalama özelliği sağlar.
    public class PaginatedList<T>
    {
        // Sayfalama için belirli bir sayfa içindeki öğelerin listesi.
        public List<T> Items { get; private set; }
        // Toplam öğe sayısı.
        public int TotalItems { get; private set; }
        // Mevcut sayfa numarası.
        public int PageNumber { get; private set; }
        // Sayfa başına düşen öğe sayısı.
        public int PageSize { get; private set; }
        // Toplam sayfa sayısı.
        public int TotalPages { get; private set; }

        // PaginatedList sınıfının constructor metodu.
        // Sayfalama işlemi için gerekli bilgileri alarak bir PaginatedList örneği oluşturur.
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Items = items;
        }

        // Bir önceki sayfanın olup olmadığını belirten özellik.
        public bool HasPreviousPage => PageNumber > 1;

        // Bir sonraki sayfanın olup olmadığını belirten özellik.
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
