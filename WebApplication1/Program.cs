using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SwiftParserService swiftParserService = new SwiftParserService();
swiftParserService.Parser("{1:F01PRCBBGSFAXXX1111111111}{2:O7991111111111ABGRSWACAXXX11111111111111111111N}{4:\r\n:20:67-C111111-KNTRL \r\n:21:30-111-1111111\r\n:79:NA VNIMANIETO NA: OTDEL BANKOVI GARANTSII\r\n.\r\nOTNOSNO: POTVARJDENIE NA AVTENTICHNOST NA\r\n         PRIDRUJITELNO PISMO KAM ISKANE ZA\r\n         PLASHTANE PO BANKOVA GARANCIA\r\n.\r\nUVAJAEMI KOLEGI,\r\n.\r\nUVEDOMJAVAME VI, CHE IZPRASHTAME ISKANE ZA \r\nPLASHTANE NA STOYNOST BGN 3.100,00, PREDSTAVENO \r\nOT NASHIA KLIENT.\r\n.\r\nS NASTOYASHTOTO POTVARZHDAVAME AVTENTICHNOSTTA NA \r\nPODPISITE VARHU PISMOTO NI, I CHE TEZI LICA SA \r\nUPALNOMOSHTENI DA PODPISVAT TAKAV DOKUMENT OT \r\nIMETO NA BANKATA AD.\r\n.\r\nPOZDRAVI,\r\nTARGOVSKO FINANSIRANE\r\n-}{5:{MAC:00000000}{CHK:111111111111}}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
