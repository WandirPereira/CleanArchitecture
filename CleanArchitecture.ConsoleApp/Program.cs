using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

//INSERÇÃO MANUAL DE DADOS

//Streamer streamer = new() { 
//    Nombre = "Amazon Prime",
//    Url = "https://www.amazonprime.com"
//};

//dbContext!.Streamers!.Add(streamer);

//await dbContext.SaveChangesAsync();

//var movies = new List<Video> {
//    new Video
//    {
//        Nombre = "Mad Max",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Batman",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Crepusculo",
//        StreamerId = streamer.Id
//    },
//    new Video
//    {
//        Nombre = "Citzen Kanex",
//        StreamerId = streamer.Id
//    },
//};

//await dbContext.AddRangeAsync(movies);
//await dbContext.SaveChangesAsync();



//REALIZANDO CONSULTAS
await TrackingAndNotTracking();
// await QueryLinq();
//await QueryMethods();
//await QueryFilter();
//QueryStreaming();
//await AddNewRecords();

Console.WriteLine("Pressione cualquer tecla para terminar el programa");
Console.ReadKey();

async Task TrackingAndNotTracking()
{
    var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
    var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking.Nombre = "Netfilx Super";
    streamerWithNoTracking.Nombre = "Amazon Plus";

    await dbContext!.SaveChangesAsync();    


}

async Task QueryLinq()
{
    Console.WriteLine($"Ingrese el servicio de streaming");
    var streamerNombre = Console.ReadLine();
    //var streamers = await (from i in dbContext.Streamers select i).ToListAsync();
    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{ streamer.Id } - {streamer.Nombre }");
    }
}


async Task QueryMethods()
{
    //var streamer1 = dbContext!.Streamers!.Where(y => y.Nombre.Contains("a")).FirstAsync();
    //var streamer2 = dbContext!.Streamers!.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();
    //var streamer3 = dbContext!.Streamers!.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    var streamer = dbContext!.Streamers!;

    //var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();
    //var firstOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();
    //var firstOrDefaultAsync_v2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));
    //var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();
    //var singleOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();
    //var resultado = await streamer.FindAsync(1);


    //Console.WriteLine($"{ firstAsync.Id } - {firstAsync.Nombre }");
    //Console.WriteLine($"{ firstOrDefaultAsync.Id } - {firstOrDefaultAsync.Nombre }");
    //Console.WriteLine($"{ firstOrDefaultAsync_v2.Id } - {firstOrDefaultAsync_v2.Nombre }");
    //Console.WriteLine($"{ singleAsync.Id } - {singleAsync.Nombre }");
    //Console.WriteLine($"{ singleOrDefaultAsync.Id } - {singleOrDefaultAsync.Nombre }");
    //Console.WriteLine($"{ resultado.Id } - {resultado.Nombre }");

    //var firstAsync = await streamer.Where(y => y.Nombre.Contains("b")).FirstAsync();
    //var firstOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("b")).FirstOrDefaultAsync();
    //var firstOrDefaultAsync_v2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("b"));
    //var singleAsync = await streamer.Where(y => y.Nombre.Contains("n")).SingleAsync();
    var singleOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("n")).SingleOrDefaultAsync();
    var resultado = await streamer.FindAsync(1);


    //Console.WriteLine($"{ firstAsync.Id } - {firstAsync.Nombre }");
    //Console.WriteLine($"{ firstOrDefaultAsync.Id } - {firstOrDefaultAsync.Nombre }");
    //Console.WriteLine($"{ firstOrDefaultAsync_v2.Id } - {firstOrDefaultAsync_v2.Nombre }");
    //Console.WriteLine($"{ singleAsync.Id } - {singleAsync.Nombre }");
    Console.WriteLine($"{ singleOrDefaultAsync.Id } - {singleOrDefaultAsync.Nombre }");
    Console.WriteLine($"{ resultado.Id } - {resultado.Nombre }");

}



async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una companhia de streaming:");
    var streamingNombre = Console.ReadLine();
    //var streamers = await dbContext!.Streamers!.Where(x => x.Nombre == streamingNombre).ToListAsync();
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre.Equals(streamingNombre)).ToListAsync();
    // var streamers = await dbContext!.Streamers!.Where(x => x.Nombre == "Netflix").ToListAsync();
    //var streamers = await dbContext!.Streamers!.ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{ streamer.Id } - {streamer.Nombre }");
    }

    //var streamerPartialresults = await dbContext!.Streamers!.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();
    var streamerPartialresults = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialresults)
    {
        Console.WriteLine($"{ streamer.Id } - {streamer.Nombre }");
    }
}


void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video{
       Nombre = "La Cenicienta",
       StreamerId = streamer.Id
    },
    new Video{
       Nombre = "1001 dalmatas",
       StreamerId = streamer.Id
    },
    new Video{
       Nombre = "El Jorobado de Notredame",
       StreamerId = streamer.Id
    },
    new Video{
       Nombre = "Star Wars",
       StreamerId = streamer.Id
    },
};

    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}




