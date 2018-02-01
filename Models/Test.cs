using System;
namespace shop.Models
{
    public class Test:BaseEntity
    {
        private string first;
        private string last;
        private String id;
        public Test()
        {

        }
        public Test(string first, string last)
        {
            this.first = first;
            this.last = last;
        }

        public string First
        {
            get { return this.first; }
            set { this.first = value; }
        }
        public String Last
        {
            get{ return this.last; }
            set{ this.last = value; }
        }
        public String Id{
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
