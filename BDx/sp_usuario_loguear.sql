USE [beraudent]
GO

DECLARE	@return_value int,
		@logueado int,
		@mensaje varchar(150)

EXEC	@return_value = [dbo].[sp_usuario_loguear]
		@nickname = N'ofaber',
		@password = N'123456',
		@logueado = @logueado OUTPUT,
		@mensaje = @mensaje OUTPUT

SELECT	@logueado as N'@logueado',
		@mensaje as N'@mensaje'

SELECT	'Return Value' = @return_value

GO
