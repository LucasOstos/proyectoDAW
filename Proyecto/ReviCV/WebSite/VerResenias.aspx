<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerResenias.aspx.cs" Inherits="VerResenias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <style>
        html, body {
            margin: 0; padding: 0; height: 100%; width: 100%;
            overflow: hidden;
            display: flex;
            justify-content: center;
            align-items: center;
            background: #a7daff;
        }
        img.fullscreen-image {
            height: 100vh;
            width: auto;
            display: block;
            margin: 0 auto;
            object-fit: contain;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="Imagenes/ConceptoRevisarReviews.png" alt="Imagen de Reseñas" class="fullscreen-image" />
        </div>
    </form>
</body>
</html>

