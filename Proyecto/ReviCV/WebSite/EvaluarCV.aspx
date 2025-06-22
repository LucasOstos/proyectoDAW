<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EvaluarCV.aspx.cs" Inherits="EvaluarCV" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Reseña de CV</title>
    <style>
        html, body {
            height: 100%;
            margin: 0;
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(to bottom right, #cceeff, #a0dcff);
            transform:scale(1.08);
        }

        .wrapper {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100%;
        }

        .container {
            display: flex;
            gap: 60px;
            padding: 40px;
            align-items: center;
            max-width: 1300px;
        }

        .cv-visual {
            background-color: white;
            border: 1px solid #ccc;
            width: 455px;
            height: 650px;
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }

        .cv-visual img {
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }

        .review-section {
            flex: 1;
            max-width: 600px;
            color: #005f9e;
            display: flex;
            flex-direction: column;
            justify-content: center;
            height: 100%;
        }

        .review-section h2 {
            font-size: 34px;
            margin-bottom: 30px;
            text-align: center;
        }

        .category {
            display: flex;
            align-items: center;
            margin-bottom: 5px;
        }

        .category span {
            font-weight: bold;
            width: 140px;
            font-size: 20px;
        }

        .rating {
            display: flex;
            flex-direction: row-reverse;
        }

        .rating:not(:checked) > input {
            position: absolute;
            appearance: none;
        }

        .rating:not(:checked) > label {
            cursor: pointer;
            font-size: 36px;
            color: #666;
            margin: 0px 0px 5px 0px;
        }

        .rating:not(:checked) > label:before {
            content: '★';
        }

        .rating > input:checked + label:hover,
        .rating > input:checked + label:hover ~ label,
        .rating > input:checked ~ label:hover,
        .rating > input:checked ~ label:hover ~ label,
        .rating > label:hover ~ input:checked ~ label {
            color: #e58e09;
        }

        .rating:not(:checked) > label:hover,
        .rating:not(:checked) > label:hover ~ label {
            color: #ff9e0b;
        }

        .rating > input:checked ~ label {
            color: #ffa723;
        }

        .comment-section {
            margin-top: 40px;
        }

        .comment-section p {
            text-align: center;
            margin-bottom: 20px;
        }

        .comment-section textarea {
            width: 100%;
            height: 120px;
            border: none;
            border-radius: 20px;
            padding: 18px;
            font-size: 17px;
            resize: none;
            font-family: inherit;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            box-sizing: border-box;
        }

        .submit-btn {
            margin-top: 25px;
            background-color: #5ec9ff;
            color: white;
            border: none;
            border-radius: 20px;
            padding: 18px 35px;
            font-size: 20px;
            cursor: pointer;
            transition: background-color 0.3s;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }

        .submit-btn:hover {
            background-color: #44b6ef;
        }

        .reviews{
            display:flex;
            flex-direction:column;
            align-items:center;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="container">
            <!-- CV Visual -->
            <div class="cv-visual">
                <img src="ruta-desde-backend.png" alt="CV de usuario" />
            </div>

            <!-- Reseña -->
            <div class="review-section">
                <h2>¿Qué opinas de este CV?</h2>

                <div class="reviews">
                <div class="category">
                    <span>Contenido</span>
                    <div class="rating">
                        <input value="5" name="contenido" id="contenido5" type="radio">
                        <label for="contenido5"></label>
                        <input value="4" name="contenido" id="contenido4" type="radio">
                        <label for="contenido4"></label>
                        <input value="3" name="contenido" id="contenido3" type="radio">
                        <label for="contenido3"></label>
                        <input value="2" name="contenido" id="contenido2" type="radio">
                        <label for="contenido2"></label>
                        <input value="1" name="contenido" id="contenido1" type="radio">
                        <label for="contenido1"></label>
                    </div>
                </div>

                <div class="category">
                    <span>Diseño</span>
                    <div class="rating">
                        <input value="5" name="diseno" id="diseno5" type="radio">
                        <label for="diseno5"></label>
                        <input value="4" name="diseno" id="diseno4" type="radio">
                        <label for="diseno4"></label>
                        <input value="3" name="diseno" id="diseno3" type="radio">
                        <label for="diseno3"></label>
                        <input value="2" name="diseno" id="diseno2" type="radio">
                        <label for="diseno2"></label>
                        <input value="1" name="diseno" id="diseno1" type="radio">
                        <label for="diseno1"></label>
                    </div>
                </div>

                <div class="category">
                    <span>Claridad</span>
                    <div class="rating">
                        <input value="5" name="claridad" id="claridad5" type="radio">
                        <label for="claridad5"></label>
                        <input value="4" name="claridad" id="claridad4" type="radio">
                        <label for="claridad4"></label>
                        <input value="3" name="claridad" id="claridad3" type="radio">
                        <label for="claridad3"></label>
                        <input value="2" name="claridad" id="claridad2" type="radio">
                        <label for="claridad2"></label>
                        <input value="1" name="claridad" id="claridad1" type="radio">
                        <label for="claridad1"></label>
                    </div>
                </div>

                <div class="category">
                    <span>Relevancia</span>
                    <div class="rating">
                        <input value="5" name="relevancia" id="relevancia5" type="radio">
                        <label for="relevancia5"></label>
                        <input value="4" name="relevancia" id="relevancia4" type="radio">
                        <label for="relevancia4"></label>
                        <input value="3" name="relevancia" id="relevancia3" type="radio">
                        <label for="relevancia3"></label>
                        <input value="2" name="relevancia" id="relevancia2" type="radio">
                        <label for="relevancia2"></label>
                        <input value="1" name="relevancia" id="relevancia1" type="radio">
                        <label for="relevancia1"></label>
                    </div>
                </div>
                </div>

                <div class="comment-section">
                    <p>Agrega un comentario adicional para ayudar a <strong>DANIELA</strong>!</p>
                    <textarea placeholder="¿Qué le recomendarías a DANIELA?"></textarea>
                    <br />
                    <button class="submit-btn">¡Enviá tu reseña!</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>