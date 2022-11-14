CREATE DATABASE AMBbancoProgII

GO
SET DATEFORMAT YMD

GO
use AMBbancoProgII

GO
CREATE TABLE CLIENTES
(
id_cliente int IDENTITY(1,1) NOT NULL,
nombre varchar(20) NOT NULL,
apellido varchar(30) NOT NULL,
dni int NOT NULL,
CONSTRAINT PK_CLIENTE PRIMARY KEY(id_cliente)
)

GO
CREATE TABLE TIPOCUENTAS
(
id_tipoCuenta int NOT NULL,
nombre varchar(40) NULL unique, 
fecha_baja datetime NULL,
 CONSTRAINT PK_TIPOCUENTAS PRIMARY KEY (id_tipoCuenta)
)

GO
CREATE TABLE CUENTAS
(
id_cuenta int IDENTITY(1,1) NOT NULL,
cbu int NOT NULL,
saldo decimal(24, 4) NULL,
ultimoMovimiento datetime NULL,
id_cliente int NULL,
id_tipoCuenta int NULL,
CONSTRAINT PK_CUENTAS PRIMARY KEY (id_cuenta),
CONSTRAINT FK_CUENTAS_TIPOCUENTAS FOREIGN KEY(id_tipoCuenta)
REFERENCES TIPOCUENTAS (id_tipoCuenta)
)
GO

--LOGIN
CREATE TABLE USUARIOS 
(
id int identity(1,1),
usuario varchar(20) unique,
pass int 
constraint pk_usuarios primary key (id)
)


----INSERT CLIENTES

GO
INSERT INTO CLIENTES (nombre, apellido, dni) VALUES ( N'Laura', N'Lopez', 34566778)
GO
INSERT INTO CLIENTES (nombre, apellido, dni)  VALUES ( N'Pilar', N'Barro', 34234578)
GO
INSERT INTO CLIENTES (nombre, apellido, dni)  VALUES (N'Rosario', N'Kleiner', 2345778)
GO
INSERT INTO CLIENTES (nombre, apellido, dni)  VALUES ( N'Laura', N'Mastreta', 31235778)
GO
INSERT INTO CLIENTES (nombre, apellido, dni)  VALUES ( N'Geronimo', N'Kleiner', 31324578)
GO
INSERT INTO CLIENTES (nombre, apellido, dni) VALUES ( N'Agus', N'mansilla', 3460115)
GO
INSERT INTO CLIENTES (nombre, apellido, dni) VALUES ( N'Sofia', N'barbiero', 3251465)
GO
INSERT INTO CLIENTES (nombre, apellido, dni) VALUES ( N'Pepis', N'barro', 34614512)




---INSERT TIPO CUENTA

INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (1, N'Caja de ahorro en pesos',null)
GO
INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (2, N'Caja de ahorro en dolares',null)
GO
INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (3, N'Cuenta corriente en pesos',null)
GO
INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (4, N'Cuenta corriente en dolares',null)
GO
INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (5, N'Cuenta sueldo',null)
GO
INSERT TIPOCUENTAS (id_tipoCuenta, nombre,fecha_baja) VALUES (6, N'Caja de ahorro recaudadora',null)



----INSERT CUENTAS

INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (123134, CAST(1500.0000 AS Decimal(24, 4)), CAST(N'2018-05-05T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (123135, CAST(15900.0000 AS Decimal(24, 4)), CAST(N'2018-05-05T00:00:00.000' AS DateTime), 1, 2)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (123136, CAST(2500.0000 AS Decimal(24, 4)), CAST(N'2018-05-05T00:00:00.000' AS DateTime), 1, 3)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES ( 215234, CAST(3000.0000 AS Decimal(24, 4)), CAST(N'2022-05-15T00:00:00.000' AS DateTime), 2, 1)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES ( 12434, CAST(1200.0000 AS Decimal(24, 4)), CAST(N'2022-09-15T00:00:00.000' AS DateTime), 3, 3)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (6765, CAST(30000.0000 AS Decimal(24, 4)), CAST(N'2022-11-15T00:00:00.000' AS DateTime), 4, 4)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES ( 6554, CAST(5000.0000 AS Decimal(24, 4)), CAST(N'2022-08-01T00:00:00.000' AS DateTime), 5, 1)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (123435576, CAST(123.0000 AS Decimal(24, 4)), CAST(N'2022-09-05T00:00:00.000' AS DateTime), 6, 1)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES ( 154652, CAST(1500.0000 AS Decimal(24, 4)), CAST(N'2022-09-07T00:00:00.000' AS DateTime), 7, 3)
GO
INSERT CUENTAS ( cbu, saldo, ultimoMovimiento, id_cliente, id_tipoCuenta) VALUES (456123, CAST(10.0000 AS Decimal(24, 4)), CAST(N'2022-09-07T00:00:00.000' AS DateTime), 8, 3)
GO

--INSERT USUARIOS
INSERT INTO USUARIOS VALUES ('Administrativo',1234)
INSERT INTO USUARIOS VALUES ('Cliente',1122)
INSERT INTO USUARIOS VALUES ('Martin Polliotto',1234)
INSERT INTO USUARIOS VALUES ('Oscar Botta',1234)
INSERT INTO USUARIOS VALUES ('Roger Martinez',1234)




---SP
GO
CREATE procedure sp_clientes_datos
as
begin
select *  from clientes 
end


GO
CREATE procedure sp_cuentas_clientes
@id_cliente int
as
begin
select cli.nombre,apellido,dni,cbu,saldo,ultimoMovimiento,cu.id_tipoCuenta,tp.nombre 'tipo'
from cuentas cu 
join clientes cli on cli.id_cliente = cu.id_cliente
join tipoCuentas tp on cu.id_tipoCuenta = tp.id_tipoCuenta
where cu.id_cliente = @id_cliente 
end

GO
create procedure sp_ConsultarCuentas
as
 begin 
select * 
from TIPOCUENTAS
where fecha_baja is null
end

GO
create procedure ProximoCliente
@next int output
as
begin
set @next = (select max(id_cliente)+1 from clientes);
end

GO
create  procedure sp_consultarCuenta
@apellidoCliente varchar(30),
@nombreCliente varchar(20),
@dni int
as 
begin
select cu.cbu,tc.nombre,cu.saldo,cu.ultimoMovimiento
from clientes c
join cuentas cu on cu.id_cliente=c.id_cliente 
join TIPOCUENTAS tc on tc.id_tipoCuenta=cu.id_tipoCuenta
where  c.apellido = @apellidoCliente and
         c.nombre =  @nombreCliente  and 
            c.dni =  @dni   
   
end

GO
 create procedure insertCuenta
 @cod_cliente int,
 @cbu int,
 @id_tipoCuenta int,
 @saldo decimal(10,2),
@ultimoMovimiento datetime
as
begin
insert into cuentas (cbu,id_tipoCuenta,saldo,ultimoMovimiento,id_cliente)
	values  (@cbu,@id_tipoCuenta, @saldo,@ultimoMovimiento,@cod_cliente)
end


GO
create procedure insertCliente
@apellido varchar(50),
@nombre varchar(50),
@dni int,
@cod_cliente int output
as
begin
insert into CLIENTES (apellido,nombre,dni)
values (@apellido,@nombre,@dni)
set @cod_cliente = SCOPE_IDENTITY();
end

GO
create procedure ProximoTipoCuenta
@next int output
as
begin
set @next = (select max(id_tipoCuenta)+1 from TIPOCUENTAS);
end

GO
create procedure sp_update_baja_tipos
@id int
as
begin
update TIPOCUENTAS set fecha_baja = GETDATE()
where id_tipoCuenta = @id
end

GO
create procedure sp_update_modificar_tipos
@id int,
@nombre varchar(100)
as
begin
update TIPOCUENTAS set nombre = @nombre
where id_tipoCuenta = @id
end

----reporte
GO
create procedure sp_saldos_cuentas
@fechaDesde datetime,
@fechaHasta datetime
as
begin
select tc.nombre 'Tipo_Cuentas',cast(sum(c.saldo) as decimal(10,2))'Saldos',cast(cast (sum(c.saldo)/
(select sum(saldo) from CUENTAS) * 100 as decimal(4,2)) as varchar)+'%' as 'Promedio'
from CUENTAS c
inner join TIPOCUENTAS tc on tc.id_tipoCuenta=c.id_tipoCuenta
where c.ultimoMovimiento between @fechaDesde and @fechaHasta
group by tc.nombre
end

--insertar tipo cuenta
GO
create procedure sp_insert_tipo
@id_tipoCuenta int,
@nombre varchar(40)
as
begin
insert into TIPOCUENTAS(id_tipoCuenta, nombre, fecha_baja) values(@id_tipoCuenta, @nombre, null)
end

GO
create procedure sp_cuentas_clientes_api
@id_cliente int
as
begin
select cli.id_cliente, cli.nombre,apellido,dni,cbu,saldo,ultimoMovimiento,cu.id_tipoCuenta,tp.nombre 'tipo'
from cuentas cu 
join clientes cli on cli.id_cliente = cu.id_cliente
join tipoCuentas tp on cu.id_tipoCuenta = tp.id_tipoCuenta
where cli.id_cliente = @id_cliente
end


GO
create procedure sp_login 
@usuario varchar(20),
@pass int,
@resultado int output
as
begin
select top 1 @resultado = count(*) from usuarios 
where usuario = @usuario and pass = @pass
end



select * from usuarios