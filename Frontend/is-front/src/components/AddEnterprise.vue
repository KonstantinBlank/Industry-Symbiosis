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
          <a class="nav-link activ" href="/">Unternehmen A - Z</a>
        </li>
        <li class="nav-item">
          <a class="nav-link activ" aria-current="page" href="/Add" >Unternehmen hinzufügen</a>
        </li>
      </ul>
        <form class="d-flex">
          <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form>
      </div>
      </div>
       </nav>
    

    <form id="InputForm">
  <!-- text -->
  <p>
    <input type="text" v-model="msg">
    {{msg}}
  </p>
  <!-- checkbox -->
  <p>
    <input type="checkbox" v-model="checked">
    {{checked ? "yes" : "no"}}
  </p>
  <!-- radio buttons -->
  <p>
    <input type="radio" name="picked" value="one" v-model="picked">
    <input type="radio" name="picked" value="two" v-model="picked">
    {{picked}}
  </p>
  <!-- select -->
  <p>
    <select v-model="selected">
      <option>one</option>
      <option>two</option>
    </select>
    {{selected}}
  </p>

</form>

      
</div>
</template>

<script>
export default {
  name: 'HomePage',
  props: {
    
  },

  // benötige Daten aus Datenbank: Adressen und Namen der Netzwerkangehörigen
    data() {
        return {
          Title      : 'Firmenname',
          checked  : true,
          picked   : 'one',
          selected : 'two',
            
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
