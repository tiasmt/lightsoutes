<template>
  <div id="app">
    
    <Home/>
  </div>
</template>

<script>
import Home from './components/Home.vue'

export default {
  name: 'App',
  components: {
    Home
  }
}
</script>

<style>
#app {
  margin: 0px;
  padding: 0px;
  box-sizing: border-box;
}

html {
  font-family: "Inter", sans-serif;
  font-weight: 500;
  font-size: 30px;
  text-rendering: optimizeLegibility;
  scroll-behavior: smooth;
  color: #2c3e50;
  background-color: transparent;
}

</style>
