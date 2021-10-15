//Criação da função “setaImagem”

function setaImagem() {

    var settings = {

        primeiraImg: function () {

            elemento = document.querySelector("#slider a:first-child");

            elemento.classList.add("ativo");

            this.legenda(elemento);

        },



        slide: function () {

            elemento = document.querySelector(".ativo");



            if (elemento.nextElementSibling) {

                elemento.nextElementSibling.classList.add("ativo");

                settings.legenda(elemento.nextElementSibling);

                elemento.classList.remove("ativo");

            } else {

                elemento.classList.remove("ativo");

                settings.primeiraImg();

            }



        },



        proximo: function () {

            clearInterval(intervalo);

            elemento = document.querySelector(".ativo");


            if (elemento.nextElementSibling) {

                elemento.nextElementSibling.classList.add("ativo");

                settings.legenda(elemento.nextElementSibling);

                elemento.classList.remove("ativo");

            } else {

                elemento.classList.remove("ativo");

                settings.primeiraImg();

            }

            intervalo = setInterval(settings.slide, 4000);

        },



        anterior: function () {

            clearInterval(intervalo);

            elemento = document.querySelector(".ativo");



            if (elemento.previousElementSibling) {

                elemento.previousElementSibling.classList.add("ativo");

                settings.legenda(elemento.previousElementSibling);

                elemento.classList.remove("ativo");

            } else {

                elemento.classList.remove("ativo");

                elemento = document.querySelector("a:last-child");

                elemento.classList.add("ativo");

                this.legenda(elemento);

            }

            intervalo = setInterval(settings.slide, 4000);

        },



        legenda: function (obj) {

            var legenda = obj.querySelector("img").getAttribute("alt");

            document.querySelector("figcaption").innerHTML = legenda;

        }



    }



    //Primeiro chamamos nossa função de setar a imagem no slideshow, depois setamos sua legenda 

    //e por fim, acionamos um temporizador que roda nosso slide a cada 4 segundos.

    //Chama o slide

    settings.primeiraImg();



    //Chama a legenda

    settings.legenda(elemento);



    //Chama o slide à um determinado tempo

    var intervalo = setInterval(settings.slide, 4000);



    //Anexando a função de clique nos controladores

    document.querySelector(".next").addEventListener("click", settings.proximo, false);

    document.querySelector(".prev").addEventListener("click", settings.anterior, false);

}



window.addEventListener("load", setaImagem, false);




