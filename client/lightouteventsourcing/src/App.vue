<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>

export default {
  name: 'App',

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
