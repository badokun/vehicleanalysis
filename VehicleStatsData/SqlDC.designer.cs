﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VehicleStats.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SQLDatabase")]
	public partial class SqlDCDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertExtractionResult(ExtractionResult instance);
    partial void UpdateExtractionResult(ExtractionResult instance);
    partial void DeleteExtractionResult(ExtractionResult instance);
    partial void InsertMake(Make instance);
    partial void UpdateMake(Make instance);
    partial void DeleteMake(Make instance);
    partial void InsertModel(Model instance);
    partial void UpdateModel(Model instance);
    partial void DeleteModel(Model instance);
    partial void InsertSourceSystem(SourceSystem instance);
    partial void UpdateSourceSystem(SourceSystem instance);
    partial void DeleteSourceSystem(SourceSystem instance);
    partial void InsertVehicle(Vehicle instance);
    partial void UpdateVehicle(Vehicle instance);
    partial void DeleteVehicle(Vehicle instance);
    #endregion
		
		public SqlDCDataContext() : 
				base(global::VehicleStats.Data.Properties.Settings.Default.SQLDatabaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDCDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDCDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDCDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlDCDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ExtractionResult> ExtractionResults
		{
			get
			{
				return this.GetTable<ExtractionResult>();
			}
		}
		
		public System.Data.Linq.Table<Make> Makes
		{
			get
			{
				return this.GetTable<Make>();
			}
		}
		
		public System.Data.Linq.Table<Model> Models
		{
			get
			{
				return this.GetTable<Model>();
			}
		}
		
		public System.Data.Linq.Table<SourceSystem> SourceSystems
		{
			get
			{
				return this.GetTable<SourceSystem>();
			}
		}
		
		public System.Data.Linq.Table<Vehicle> Vehicles
		{
			get
			{
				return this.GetTable<Vehicle>();
			}
		}
		
		public System.Data.Linq.Table<ExtractionResultsView> ExtractionResultsViews
		{
			get
			{
				return this.GetTable<ExtractionResultsView>();
			}
		}
		
		public System.Data.Linq.Table<MakeModelView> MakeModelViews
		{
			get
			{
				return this.GetTable<MakeModelView>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ExtractionResults")]
	public partial class ExtractionResult : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _ModelId;
		
		private int _From;
		
		private int _To;
		
		private System.DateTime _ExtractionDateTime;
		
		private EntitySet<Vehicle> _Vehicles;
		
		private EntityRef<Model> _Model;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnModelIdChanging(int value);
    partial void OnModelIdChanged();
    partial void OnFromChanging(int value);
    partial void OnFromChanged();
    partial void OnToChanging(int value);
    partial void OnToChanged();
    partial void OnExtractionDateTimeChanging(System.DateTime value);
    partial void OnExtractionDateTimeChanged();
    #endregion
		
		public ExtractionResult()
		{
			this._Vehicles = new EntitySet<Vehicle>(new Action<Vehicle>(this.attach_Vehicles), new Action<Vehicle>(this.detach_Vehicles));
			this._Model = default(EntityRef<Model>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModelId", DbType="Int NOT NULL")]
		public int ModelId
		{
			get
			{
				return this._ModelId;
			}
			set
			{
				if ((this._ModelId != value))
				{
					if (this._Model.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnModelIdChanging(value);
					this.SendPropertyChanging();
					this._ModelId = value;
					this.SendPropertyChanged("ModelId");
					this.OnModelIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[From]", Storage="_From", DbType="Int NOT NULL")]
		public int From
		{
			get
			{
				return this._From;
			}
			set
			{
				if ((this._From != value))
				{
					this.OnFromChanging(value);
					this.SendPropertyChanging();
					this._From = value;
					this.SendPropertyChanged("From");
					this.OnFromChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[To]", Storage="_To", DbType="Int NOT NULL")]
		public int To
		{
			get
			{
				return this._To;
			}
			set
			{
				if ((this._To != value))
				{
					this.OnToChanging(value);
					this.SendPropertyChanging();
					this._To = value;
					this.SendPropertyChanged("To");
					this.OnToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExtractionDateTime", DbType="DateTime NOT NULL")]
		public System.DateTime ExtractionDateTime
		{
			get
			{
				return this._ExtractionDateTime;
			}
			set
			{
				if ((this._ExtractionDateTime != value))
				{
					this.OnExtractionDateTimeChanging(value);
					this.SendPropertyChanging();
					this._ExtractionDateTime = value;
					this.SendPropertyChanged("ExtractionDateTime");
					this.OnExtractionDateTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ExtractionResult_Vehicle", Storage="_Vehicles", ThisKey="Id", OtherKey="ExtractionResults_FK")]
		public EntitySet<Vehicle> Vehicles
		{
			get
			{
				return this._Vehicles;
			}
			set
			{
				this._Vehicles.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Model_ExtractionResult", Storage="_Model", ThisKey="ModelId", OtherKey="Id", IsForeignKey=true)]
		public Model Model
		{
			get
			{
				return this._Model.Entity;
			}
			set
			{
				Model previousValue = this._Model.Entity;
				if (((previousValue != value) 
							|| (this._Model.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Model.Entity = null;
						previousValue.ExtractionResults.Remove(this);
					}
					this._Model.Entity = value;
					if ((value != null))
					{
						value.ExtractionResults.Add(this);
						this._ModelId = value.Id;
					}
					else
					{
						this._ModelId = default(int);
					}
					this.SendPropertyChanged("Model");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.ExtractionResult = this;
		}
		
		private void detach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.ExtractionResult = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Make")]
	public partial class Make : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _MakeName;
		
		private System.DateTime _InsertDate;
		
		private int _SourceSystemId;
		
		private EntitySet<Model> _Models;
		
		private EntityRef<SourceSystem> _SourceSystem;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnMakeNameChanging(string value);
    partial void OnMakeNameChanged();
    partial void OnInsertDateChanging(System.DateTime value);
    partial void OnInsertDateChanged();
    partial void OnSourceSystemIdChanging(int value);
    partial void OnSourceSystemIdChanged();
    #endregion
		
		public Make()
		{
			this._Models = new EntitySet<Model>(new Action<Model>(this.attach_Models), new Action<Model>(this.detach_Models));
			this._SourceSystem = default(EntityRef<SourceSystem>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MakeName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string MakeName
		{
			get
			{
				return this._MakeName;
			}
			set
			{
				if ((this._MakeName != value))
				{
					this.OnMakeNameChanging(value);
					this.SendPropertyChanging();
					this._MakeName = value;
					this.SendPropertyChanged("MakeName");
					this.OnMakeNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_InsertDate", DbType="DateTime NOT NULL")]
		public System.DateTime InsertDate
		{
			get
			{
				return this._InsertDate;
			}
			set
			{
				if ((this._InsertDate != value))
				{
					this.OnInsertDateChanging(value);
					this.SendPropertyChanging();
					this._InsertDate = value;
					this.SendPropertyChanged("InsertDate");
					this.OnInsertDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceSystemId", DbType="Int NOT NULL")]
		public int SourceSystemId
		{
			get
			{
				return this._SourceSystemId;
			}
			set
			{
				if ((this._SourceSystemId != value))
				{
					if (this._SourceSystem.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSourceSystemIdChanging(value);
					this.SendPropertyChanging();
					this._SourceSystemId = value;
					this.SendPropertyChanged("SourceSystemId");
					this.OnSourceSystemIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Make_Model", Storage="_Models", ThisKey="Id", OtherKey="Make_FK")]
		public EntitySet<Model> Models
		{
			get
			{
				return this._Models;
			}
			set
			{
				this._Models.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SourceSystem_Make", Storage="_SourceSystem", ThisKey="SourceSystemId", OtherKey="Id", IsForeignKey=true)]
		public SourceSystem SourceSystem
		{
			get
			{
				return this._SourceSystem.Entity;
			}
			set
			{
				SourceSystem previousValue = this._SourceSystem.Entity;
				if (((previousValue != value) 
							|| (this._SourceSystem.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._SourceSystem.Entity = null;
						previousValue.Makes.Remove(this);
					}
					this._SourceSystem.Entity = value;
					if ((value != null))
					{
						value.Makes.Add(this);
						this._SourceSystemId = value.Id;
					}
					else
					{
						this._SourceSystemId = default(int);
					}
					this.SendPropertyChanged("SourceSystem");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Models(Model entity)
		{
			this.SendPropertyChanging();
			entity.Make = this;
		}
		
		private void detach_Models(Model entity)
		{
			this.SendPropertyChanging();
			entity.Make = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Model")]
	public partial class Model : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _Make_FK;
		
		private string _ModelName;
		
		private System.DateTime _InsertDate;
		
		private EntitySet<ExtractionResult> _ExtractionResults;
		
		private EntityRef<Make> _Make;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnMake_FKChanging(int value);
    partial void OnMake_FKChanged();
    partial void OnModelNameChanging(string value);
    partial void OnModelNameChanged();
    partial void OnInsertDateChanging(System.DateTime value);
    partial void OnInsertDateChanged();
    #endregion
		
		public Model()
		{
			this._ExtractionResults = new EntitySet<ExtractionResult>(new Action<ExtractionResult>(this.attach_ExtractionResults), new Action<ExtractionResult>(this.detach_ExtractionResults));
			this._Make = default(EntityRef<Make>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Make_FK", DbType="Int NOT NULL")]
		public int Make_FK
		{
			get
			{
				return this._Make_FK;
			}
			set
			{
				if ((this._Make_FK != value))
				{
					if (this._Make.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMake_FKChanging(value);
					this.SendPropertyChanging();
					this._Make_FK = value;
					this.SendPropertyChanged("Make_FK");
					this.OnMake_FKChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModelName", DbType="VarChar(350) NOT NULL", CanBeNull=false)]
		public string ModelName
		{
			get
			{
				return this._ModelName;
			}
			set
			{
				if ((this._ModelName != value))
				{
					this.OnModelNameChanging(value);
					this.SendPropertyChanging();
					this._ModelName = value;
					this.SendPropertyChanged("ModelName");
					this.OnModelNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_InsertDate", DbType="DateTime NOT NULL")]
		public System.DateTime InsertDate
		{
			get
			{
				return this._InsertDate;
			}
			set
			{
				if ((this._InsertDate != value))
				{
					this.OnInsertDateChanging(value);
					this.SendPropertyChanging();
					this._InsertDate = value;
					this.SendPropertyChanged("InsertDate");
					this.OnInsertDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Model_ExtractionResult", Storage="_ExtractionResults", ThisKey="Id", OtherKey="ModelId")]
		public EntitySet<ExtractionResult> ExtractionResults
		{
			get
			{
				return this._ExtractionResults;
			}
			set
			{
				this._ExtractionResults.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Make_Model", Storage="_Make", ThisKey="Make_FK", OtherKey="Id", IsForeignKey=true)]
		public Make Make
		{
			get
			{
				return this._Make.Entity;
			}
			set
			{
				Make previousValue = this._Make.Entity;
				if (((previousValue != value) 
							|| (this._Make.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Make.Entity = null;
						previousValue.Models.Remove(this);
					}
					this._Make.Entity = value;
					if ((value != null))
					{
						value.Models.Add(this);
						this._Make_FK = value.Id;
					}
					else
					{
						this._Make_FK = default(int);
					}
					this.SendPropertyChanged("Make");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ExtractionResults(ExtractionResult entity)
		{
			this.SendPropertyChanging();
			entity.Model = this;
		}
		
		private void detach_ExtractionResults(ExtractionResult entity)
		{
			this.SendPropertyChanging();
			entity.Model = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SourceSystem")]
	public partial class SourceSystem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private EntitySet<Make> _Makes;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public SourceSystem()
		{
			this._Makes = new EntitySet<Make>(new Action<Make>(this.attach_Makes), new Action<Make>(this.detach_Makes));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SourceSystem_Make", Storage="_Makes", ThisKey="Id", OtherKey="SourceSystemId")]
		public EntitySet<Make> Makes
		{
			get
			{
				return this._Makes;
			}
			set
			{
				this._Makes.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Makes(Make entity)
		{
			this.SendPropertyChanging();
			entity.SourceSystem = this;
		}
		
		private void detach_Makes(Make entity)
		{
			this.SendPropertyChanging();
			entity.SourceSystem = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Vehicle")]
	public partial class Vehicle : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Title;
		
		private int _Year;
		
		private int _Milage;
		
		private decimal _Price;
		
		private int _ExtractionResults_FK;
		
		private EntityRef<ExtractionResult> _ExtractionResult;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnYearChanging(int value);
    partial void OnYearChanged();
    partial void OnMilageChanging(int value);
    partial void OnMilageChanged();
    partial void OnPriceChanging(decimal value);
    partial void OnPriceChanged();
    partial void OnExtractionResults_FKChanging(int value);
    partial void OnExtractionResults_FKChanged();
    #endregion
		
		public Vehicle()
		{
			this._ExtractionResult = default(EntityRef<ExtractionResult>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="VarChar(150) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int NOT NULL")]
		public int Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this.OnYearChanging(value);
					this.SendPropertyChanging();
					this._Year = value;
					this.SendPropertyChanged("Year");
					this.OnYearChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Milage", DbType="Int NOT NULL")]
		public int Milage
		{
			get
			{
				return this._Milage;
			}
			set
			{
				if ((this._Milage != value))
				{
					this.OnMilageChanging(value);
					this.SendPropertyChanging();
					this._Milage = value;
					this.SendPropertyChanged("Milage");
					this.OnMilageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Decimal(18,2) NOT NULL")]
		public decimal Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this.OnPriceChanging(value);
					this.SendPropertyChanging();
					this._Price = value;
					this.SendPropertyChanged("Price");
					this.OnPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExtractionResults_FK", DbType="Int NOT NULL")]
		public int ExtractionResults_FK
		{
			get
			{
				return this._ExtractionResults_FK;
			}
			set
			{
				if ((this._ExtractionResults_FK != value))
				{
					if (this._ExtractionResult.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnExtractionResults_FKChanging(value);
					this.SendPropertyChanging();
					this._ExtractionResults_FK = value;
					this.SendPropertyChanged("ExtractionResults_FK");
					this.OnExtractionResults_FKChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ExtractionResult_Vehicle", Storage="_ExtractionResult", ThisKey="ExtractionResults_FK", OtherKey="Id", IsForeignKey=true)]
		public ExtractionResult ExtractionResult
		{
			get
			{
				return this._ExtractionResult.Entity;
			}
			set
			{
				ExtractionResult previousValue = this._ExtractionResult.Entity;
				if (((previousValue != value) 
							|| (this._ExtractionResult.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ExtractionResult.Entity = null;
						previousValue.Vehicles.Remove(this);
					}
					this._ExtractionResult.Entity = value;
					if ((value != null))
					{
						value.Vehicles.Add(this);
						this._ExtractionResults_FK = value.Id;
					}
					else
					{
						this._ExtractionResults_FK = default(int);
					}
					this.SendPropertyChanged("ExtractionResult");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ExtractionResultsView")]
	public partial class ExtractionResultsView
	{
		
		private int _Id;
		
		private string _SourceSystem;
		
		private string _MakeName;
		
		private string _ModelName;
		
		private int _From;
		
		private int _To;
		
		public ExtractionResultsView()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL")]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceSystem", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SourceSystem
		{
			get
			{
				return this._SourceSystem;
			}
			set
			{
				if ((this._SourceSystem != value))
				{
					this._SourceSystem = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MakeName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string MakeName
		{
			get
			{
				return this._MakeName;
			}
			set
			{
				if ((this._MakeName != value))
				{
					this._MakeName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModelName", DbType="VarChar(350) NOT NULL", CanBeNull=false)]
		public string ModelName
		{
			get
			{
				return this._ModelName;
			}
			set
			{
				if ((this._ModelName != value))
				{
					this._ModelName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[From]", Storage="_From", DbType="Int NOT NULL")]
		public int From
		{
			get
			{
				return this._From;
			}
			set
			{
				if ((this._From != value))
				{
					this._From = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[To]", Storage="_To", DbType="Int NOT NULL")]
		public int To
		{
			get
			{
				return this._To;
			}
			set
			{
				if ((this._To != value))
				{
					this._To = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MakeModelView")]
	public partial class MakeModelView
	{
		
		private string _SourceSystem;
		
		private string _MakeName;
		
		private string _ModelName;
		
		public MakeModelView()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceSystem", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SourceSystem
		{
			get
			{
				return this._SourceSystem;
			}
			set
			{
				if ((this._SourceSystem != value))
				{
					this._SourceSystem = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MakeName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string MakeName
		{
			get
			{
				return this._MakeName;
			}
			set
			{
				if ((this._MakeName != value))
				{
					this._MakeName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ModelName", DbType="VarChar(350) NOT NULL", CanBeNull=false)]
		public string ModelName
		{
			get
			{
				return this._ModelName;
			}
			set
			{
				if ((this._ModelName != value))
				{
					this._ModelName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591