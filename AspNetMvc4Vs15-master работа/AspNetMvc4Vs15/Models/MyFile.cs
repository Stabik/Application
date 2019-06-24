using System.IO;

namespace AspNetMvc4Vs15
{
    public class MyFile
    {
        public virtual int Id { get; set; }
        public virtual byte[] MyDocFile { get; set; }
        public virtual string MyDocName { get; set; }
        public virtual Document Document { get; set; }
    
    }
}