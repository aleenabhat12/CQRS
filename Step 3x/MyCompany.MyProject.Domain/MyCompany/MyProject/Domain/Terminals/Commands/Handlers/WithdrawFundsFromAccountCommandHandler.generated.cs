﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region Copyright
// -----------------------------------------------------------------------
// <copyright company="cdmdotnet Limited">
//     Copyright cdmdotnet Limited. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
#endregion
using Cqrs.Domain;
using MyCompany.MyProject.Domain.Terminals;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cqrs.Commands;
using Cqrs.Configuration;
using Cqrs.Domain;
using Cqrs.Domain.Exceptions;
using Cqrs.Events;
using cdmdotnet.Logging;

namespace MyCompany.MyProject.Domain.Terminals.Commands.Handlers
{
	[GeneratedCode("CQRS UML Code Generator", "1.601.786")]
	public  partial class WithdrawFundsFromAccountCommandHandler
		
		: ICommandHandler<Cqrs.Authentication.ISingleSignOnToken, WithdrawFundsFromAccountCommand>
	{
		protected IUnitOfWork<Cqrs.Authentication.ISingleSignOnToken> UnitOfWork { get; private set; }

		protected IDependencyResolver DependencyResolver { get; private set; }

		protected ILogger Logger { get; private set; }

		public WithdrawFundsFromAccountCommandHandler(IUnitOfWork<Cqrs.Authentication.ISingleSignOnToken> unitOfWork, IDependencyResolver dependencyResolver, ILogger logger)
		{
			UnitOfWork = unitOfWork;
			DependencyResolver = dependencyResolver;
			Logger = logger;
		}


		#region Implementation of ICommandHandler<in WithdrawFundsFromAccount>

		public void Handle(WithdrawFundsFromAccountCommand command)
		{
			ICommandValidator<Cqrs.Authentication.ISingleSignOnToken, WithdrawFundsFromAccountCommand> commandValidator = null;
			try
			{
				commandValidator = DependencyResolver.Resolve<ICommandValidator<Cqrs.Authentication.ISingleSignOnToken, WithdrawFundsFromAccountCommand>>();
			}
			catch (Exception exception)
			{
				Logger.LogDebug("Locating an ICommandValidator failed.", "WithdrawFundsFromAccountCommandHandler\\Handle(WithdrawFundsFromAccountCommand)", exception);
			}

			if (commandValidator != null && !commandValidator.IsCommandValid(command))
			{
				Logger.LogInfo("The provided command is not valid.", "WithdrawFundsFromAccountCommandHandler\\Handle(WithdrawFundsFromAccountCommand)");
				return;
			}

			bool continueExecution = true;
			OnHandle(command, ref continueExecution);
			if (continueExecution)
			{
				Account item = null;
				OnWithdrawFunds(command, ref item);
				if (item == null)
				{
					item = new Account(DependencyResolver, Logger, command.Rsn);
					UnitOfWork.Add(item);
				}
				item.WithdrawFunds(command.Amount);
				OnWithdrawFundsHandled(command, item);
				OnAddToUnitOfWork(command, item);
				UnitOfWork.Add(item);
				OnAddedToUnitOfWork(command, item);
				OnCommit(command, item);
				UnitOfWork.Commit();
				OnCommited(command, item);
			}
		}

		#endregion

		partial void OnHandle(WithdrawFundsFromAccountCommand command, ref bool continueExecution);

		partial void OnWithdrawFunds(WithdrawFundsFromAccountCommand command, ref Account item);

		partial void OnWithdrawFundsHandled(WithdrawFundsFromAccountCommand command, Account item);

		partial void OnAddToUnitOfWork(WithdrawFundsFromAccountCommand command, Account item);

		partial void OnAddedToUnitOfWork(WithdrawFundsFromAccountCommand command, Account item);

		partial void OnCommit(WithdrawFundsFromAccountCommand command, Account item);

		partial void OnCommited(WithdrawFundsFromAccountCommand command, Account item);
	}
}
