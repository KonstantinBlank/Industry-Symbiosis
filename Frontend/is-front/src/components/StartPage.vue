<template>
  <div class="hello">
      <nav class="navbar sticky-top navbar-expand-lg navbar-light" style="background-color: #D3D3D3;">
        <a class="navbar-brand" href="#">Industry Symbiose 5.0</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item active">
              <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
            </li>
           
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
              Unternehmen
              </a>
              <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                <a class="dropdown-item" href="#">Übersicht A-Z</a>
                <div class="dropdown-divider"></div>
                <a class="dropdown-item" href="#">Neues Unternehmen anlegen</a>
              </div>
            </li>
          </ul>
          <form class="form-inline my-2 my-lg-0">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
          </form>
        </div>
      </nav>

      <div class="background">

        <ul class="list-group list-group-flush">
 
            <li class="list-group-item" v-for="(item, i) in items" v-bind:key="item">
                {{item.Fname}} - {{i}}
                {{item.Fstraße}} 
                {{item.Fnummer}} 
            </li>
        </ul>
      </div>




</div>


</template>

<script>
export default {
  name: 'StartPage',
  props: {
    msg: String
  },

// benötige Daten aus Datenbank: Adressen und Namen der Netzwerkangehörigen
    data() {
        return {
            items: [
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
                {
                    Fname: 'Motzner Straße',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '1',
                }, 
            ]
        }
    }
}
</script>



<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

.background {
  background-image: none;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center center;
  padding-bottom: 50%;
}

button{
  background-color: rgb(0, 50, 14);
  color: white;
  margin: 4% 4%;

}

.card{
  padding: 30px 40px;
  margin-top: 60px;
  margin-bottom: 60px;
  border: none !important;
  box-shadow: 0 6px 12px 0 rgba(0,0,0,0.2)
  
  }

.topic {
  color: white;
}


.welcome-text{
  color: white;
  }
  
.form-control-label{
  margin-bottom: 0
  }

input, textarea, button{
    padding: 8px 15px;
    border-radius: 5px !important;
    margin: 5px 0px;box-sizing: border-box;
    border: 1px solid #ccc;
    font-size: 18px !important;
    font-weight: 300
    }
  
input:focus, textarea:focus{
    -moz-box-shadow: none !important;
    -webkit-box-shadow: none !important;
    box-shadow: none !important;
    border: 1px solid #00BCD4;
    outline-width: 0;
    font-weight: 400
    }
    
.btn-block{
  text-transform: uppercase;
  font-size: 15px !important;
  font-weight: 400;
  height: 43px;
  cursor: pointer
  }
  
  .btn-block:hover{
    color: #fff !important
    }
    
button:focus{
-moz-box-shadow: none !important;
-webkit-box-shadow: none !important;
box-shadow: none !important;
outline-width: 0
}

.nav-link {
  color: #000;
}


</style>
