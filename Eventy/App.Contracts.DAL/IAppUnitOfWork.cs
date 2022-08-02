﻿using Base.Contracts.DAL;
using App.Contracts.DAL.Repositories;


namespace App.Contracts.DAL;


/// <summary>
/// App Specific Unit of Work Design Pattern.
///  - Defines Repos That Should Be Implemented.
/// </summary>
public interface IAppUnitOfWork : IUnitOfWork
{
    
    /// <summary>
    /// Data Access Layer Repository Definition For Storing Events.
    /// </summary>
    IEventRepository Events { get; }
    
}