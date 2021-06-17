using System;
using System.IO;
using System.Reflection;

namespace TestRestAPI
{
	public class Settings
		{
			public Settings()
			{
				_basePath = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName;
				

			}

			private string _basePath;

			public string BasePath
			{
				get => _basePath;
				set => _basePath = value;
			}


			private string _token;
			private string _isTest;

			private string _orgUrl;
			private string _organization;
			private string _project;
			private string _apiVersion;
			private string _itemPath;
			private string _pathIDsWorkItems;
			private string _pathAnonymezedNames;
			private string _task;

			public string Task
			{
				get =>  _task ?? "";
				set => _task = value;
			}

			public string Token
			{
				get => _token ?? "d5egfq2hjhcvn7xhxzapc7bckp3sz2yu6akx4jpdo4gf7qxfns2q";
				set => _token = value;
			}

			public string IsTest
			{
				get => _isTest;
				set => _isTest = value;
			}

			public string OrgUrl
			{
				get => _orgUrl ?? "https://tfs.bk.datev.de/tfs/";
				set => _orgUrl = value;
			}

			public string Organization
			{
				get => _organization ?? "c3";
				set => _organization = value;
			}

			public string Project
			{
				get => _project ?? "OnPremise";
				set => _project = value;
			}

			public string ApiVersion
			{
				get => _apiVersion ?? "6.0";
				set => _apiVersion = value;
			}

			public string ItemPath
			{
				get => _itemPath ?? "searchCriteria.itemPath=%24%2FOnPremise%2FOnPremise%2Fdev%2Fdev-main%2FPWS%2FLUG&";
				set => _itemPath = value;
			}

			public string PathAnonymezedNames
			{
				get => _pathAnonymezedNames ?? Path.Combine(_basePath, "anonymousNames.csv");
				set => _pathAnonymezedNames = value;
			}


			public string PathIDsWorkItems
			{
				get => _pathIDsWorkItems ?? Path.Combine(_basePath, "workitemsIds.csv");
				set => _pathIDsWorkItems = value;
			}
			
		}
	}
