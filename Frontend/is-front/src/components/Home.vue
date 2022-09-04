<template>

<div>
<nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Industry Symbisis 5.0</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
        </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link activ" aria-current="page" href="/">Unternehmen A - Z</a>
        </li>
        <li class="nav-item">
          <a class="nav-link activ" href="/Add" >Unternehmen hinzufügen</a>
        </li>
      </ul>
        <form class="d-flex">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
      </div>
      </div>
       </nav>


       <div class="container p-4">
          <div class="accordion" id="accordionExample">
            <div class="accordion-item" v-for="(item) in items" :key="item.id">
              <h2 class="accordion-header" :id="'heading'+item">
              <!-- <button class="accordion-button" :class="{ 'collapsed': index !== 0 }" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapse'+item.id" aria-expanded="true" :aria-controls="'collapse'+item"> -->
              <button class="accordion-button" :class="{ 'collapsed': true }" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapse'+item.id" aria-expanded="true" :aria-controls="'collapse'+item">
                <strong>
                  {{item.Fname}}
                </strong>
              </button>
              </h2>
            <!-- <div :id="'collapse'+item.id" class="accordion-collapse collapse" :class="{ 'show': index === 0 }" :aria-labelledby="'heading'+item" data-bs-parent="#accordionExample"> -->
            <div :id="'collapse'+item.id" class="accordion-collapse collapse" :class="{ 'show': false }" :aria-labelledby="'heading'+item" data-bs-parent="#accordionExample">
              <div class="accordion-body">
                <span>{{item.Fstraße}}</span>
              </div>
            </div>
        </div>
      </div>
    </div>
</div>
</template>

<script>
export default {
  name: 'HomePage',
  props: {
    msg: String
  },

  // benötige Daten aus Datenbank: Adressen und Namen der Netzwerkangehörigen
    data() {
        return {
      
            items: [
                {
                    id: 0,
                    Fname: 'Bier AG',
                    Fstraße: 'August-Merzer-Allee',
                    Fnummer: '49',
                }, 
                {
                    id: 1,
                    Fname: 'Wagenmeier Stoffe',
                    Fstraße: ' Jauselstraße',
                    Fnummer: '34',
                }, 
                {
                    id: 2,
                    Fname: 'Die Firma GmbH',
                    Fstraße: 'Motzner Straße',
                    Fnummer: '15',
                }, 
                {
                    id: 3,
                    Fname: 'Capitol Versicherungen',
                    Fstraße: 'Bossweg',
                    Fnummer: '1',
                }, 
                {
                    id: 4,
                    Fname: 'Vermögensberatung Keineahnung u. Söhne ',
                    Fstraße: 'Dummschwätzerweg',
                    Fnummer: '100',
                }, 
                {
                    id: 5,
                    Fname: 'Lichter Lampen & Kühlschränke KG',
                    Fstraße: 'Richterstraße',
                    Fnummer: '5',
                }, 
          
              ]
            }
    },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
[aria-current="page"] {

  border-bottom: 2px solid rgb(0, 50, 14);

}

</style>
