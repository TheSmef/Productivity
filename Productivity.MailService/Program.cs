using Productivity.MailService.Services;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Services.Interfaces;
using Productivity.Shared.Services;
using Productivity.MailService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Productivity.MailService.Data.Repositories.Interfaces;
using Productivity.MailService.Data.Repositories;
using Productivity.MailService.Services.Queue.Interfaces;
using Productivity.MailService.Services.Queue;
using Productivity.Shared.Utility.AutoMapper;
using Productivity.MailService.Services.Interfaces;
using Productivity.MailService.Services.Sender.Interfaces;
using Productivity.MailService.Services.Sender;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MailWorker>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.Configure<RabbitMqConfiguration>(a => builder.Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));

builder.Services.Configure<SMTPConfiguration>(a => builder.Configuration.GetSection(nameof(SMTPConfiguration)).Bind(a));

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

builder.Services.AddScoped<IMailRepository, MailRepository>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddScoped<IMailSenderService, MailSenderService>();



builder.Services.AddSingleton<IMailQueueService, MailQueueService>();

var host = builder.Build();
host.Run();
