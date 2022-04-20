//.NET
global using System;
global using Microsoft.Extensions.Options;

//NUGET
global using Cronos;

global using FluentValidation;
global using FluentValidation.AspNetCore;

global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using SendGrid;
global using SendGrid.Helpers.Mail;

//API
global using backendProject.API.Configuration;
global using backendProject.API.Context;
global using backendProject.API.GraphQl.Movies;
global using backendProject.API.GraphQl.Queries;
global using backendProject.API.Models;
global using backendProject.API.Repositories;
global using backendProject.API.Services;
global using backendProject.API.Validators;