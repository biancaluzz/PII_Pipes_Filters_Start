using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            PipeNull pipe3 = new PipeNull();

            IFilter filterTwitter = new FilterConditional();
            PipeSerial pipeTwitter = new PipeSerial(filterTwitter, pipe3);

            IFilter filter2 = new FilterNegative(); 
            PipeSerial pipe2 = new PipeSerial(filter2, pipeTwitter);

            IFilter filterPersist = new FilterPersist();
            PipeSerial pipePersist = new PipeSerial(filterPersist, pipe2);

            IFilter filter1 = new FilterGreyscale();
            PipeSerial pipe1 = new PipeSerial(filter1, pipePersist);

            IPicture sendPicture = pipe1.Send(picture);
        }
    }
}

