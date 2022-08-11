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
            <li class="nav-item">
              <a class="nav-link disabled" href="#">Disabled</a>
            </li>
          </ul>
          <form class="form-inline my-2 my-lg-0">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
          </form>
        </div>
      </nav>

      <div class="background">
          <div class="container-fluid px-1 py-5 mx-auto">
            <div class="row d-flex justify-content-center">
              <div class="col-xl-7 col-lg-8 col-md-9 col-11 text-center">
                <h3 class="topic">Login</h3>
                <p class="welcome-text">Bitte melden Sie sich mit<br> Ihrem Anmeldenamen und Passwort an.</p>
                <div class="card">
                <h5 class="text-center mb-4">Admin Login</h5>
                  <form class="form-card" onsubmit="event.preventDefault()">
                    <div class="row justify-content-between text-left">
                        <div class="form-group col-sm-6 flex-column d-flex"> 
                          <label class="form-control-label px-3">Anmeldename<span class="text-danger"> *</span></label> 
                          <input type="text" id="fname" name="fname" placeholder="Anmeldename" onblur="validate(1)"> 
                        </div>
                        <div class="form-group col-sm-6 flex-column d-flex"> <label class="form-control-label px-3">Passwort<span class="text-danger"> *</span></label> 
                          <input type="text" id="lpass" name="lpass" placeholder="Passwort" onblur="validate(2)"> 
                        </div>
                    </div>
                    
                        <div class="form-group col-sm-12 text-center flex-column d-flex">
                          <button @click="$router.push('StartPage')">Anmelden</button>
                        </div>
              
                    <div class="row justify-content-between">
                      <div class="form-group col-sm-6 flex-column text-left">
                        <span class="text-danger"> * </span><span>Pflichtfeld</span>
                      </div>
                      <div class="form-group col-sm-6 flex-column d-flex text-right">
                        <router-link to="/ForgetPassword">Passwort vergessen</router-link>
                      </div>
                    </div>
                    
                  </form>
                </div>
              </div>
            </div>
          </div>
      </div>




</div>


</template>

<script>
export default {
  name: 'LoginPage',
  props: {
    msg: String
  },
}
</script>



<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

.background {
  background-image: url('~@/assets/Background1.jpeg');
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

.btn {
  background-color: white;
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
