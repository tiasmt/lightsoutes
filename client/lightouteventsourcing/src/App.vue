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

html, input {
  font-family: "Inter", sans-serif;
  font-weight: 500;
  font-size: 30px;
  text-rendering: optimizeLegibility;
  scroll-behavior: smooth;
  color: #eff7fc;
  background-color: rgb(92, 90, 90);
}

*:focus {
    outline: none;
}

</style>
