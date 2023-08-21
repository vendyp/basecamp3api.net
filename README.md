# Basecamp 4 API .NET

[![.NET CI](https://github.com/vendyp/basecamp3api.net/actions/workflows/ci.yml/badge.svg)](https://github.com/vendyp/basecamp3api.net/actions/workflows/ci.yml)
[![.NET Release](https://github.com/vendyp/basecamp3api.net/actions/workflows/release.yml/badge.svg)](https://github.com/vendyp/basecamp3api.net/actions/workflows/release.yml)
[![NuGet](https://img.shields.io/nuget/vpre/Basecamp3Api.svg)](https://www.nuget.org/packages/Basecamp3Api)

This is a open-source library for integration with Basecamp 4 API [here](https://github.com/basecamp/bc3-api)

How to register your application, you can jump
on [Here](https://github.com/vendyp/basecamp3api.net/blob/main/HOWTOREGISTER.md)

This repository is still inprogress :)

# How to use

This library is not helping you to do OAuth2 out-of-the-box, you have to make it on your own

First, you need to setup a static configuration

```csharp
// instantiate baseapiclient
var setting = new BasecampApiSetting()
{
    ClientId = "***",
    ClientSecret = "***",
    AppName = "***",
    RedirectUrl = new Uri("***")
}
var client = new BasecampApiClient(setting);

//after OAuth2 authentication, pass those values to this method
client.Setup(
    token.AccessToken, 
    token.ExpiresIn, 
    token.RefreshToken);

//then you can consume the API
(PagedList<Project>? List, Error? Error) result = await client.GetAllProjectAsync(99999, 1, cancellationToken);
```

# Supported API

## Authentication

Docs [Here](https://github.com/basecamp/api/blob/master/sections/authentication.md)

* GenerateToken (after callback/redirect url)
* GetAuthorization (get detail account)
* GetLoginUrl (construct login url)
* RefreshToken

## Projects

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/projects.md#projects)

* Create Project
* Get All Project
* Get Project
* Update Project
* Trash Project

## Todosets

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/todosets.md)

* Get Todoset

## Todolists

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/todolists.md)

* Create Todolists
* Update Todolists
* Get all Todolists
* Get Todolists
* Archive Todolists
* Unarchive Todolists
* Trash Todolists

## Todos

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/todos.md)

* Create Todos
* Update Todos
* Complete Todos
* Uncomplete Todos
* Get All Todos
* Get Todos
* Reposition Todos

## Peoples

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/people.md)

* Get all people
* Get all people in project
* Get all pingable people
* Get my personal information
* Get people by id
* Update who can access project (grant/revoke/create)

## Recordings

Docs [here](https://github.com/basecamp/bc3-api/blob/master/sections/recordings.md)

* Get recordings
* Archive recording
* Unarchive recording
* Trash recording

# Change Log

Change log detail are [here](https://github.com/vendyp/basecamp3api.net/blob/main/CHANGELOG.md)