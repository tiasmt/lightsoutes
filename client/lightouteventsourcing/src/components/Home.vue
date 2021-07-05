<template>
  <div class="home">
    <div class="head">
      <img alt="Lights On" src="../assets/images/ON.svg" />
      <h3>VS</h3>
      <img alt="Lights Off" src="../assets/images/OFF.svg" />
    </div>
    <div class="options">
      <a class="button create" @click="CreateGame()">New Game</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
  data() {
    return {};
  },
  methods: {
    CreateGame() {
      this.$store.dispatch("CreateGame");
    },
  },
  created() {
      this.$gameHub.$on("update-game", (data) => {
      this.$store.dispatch("Update", data);
    });
  },
  beforeDestroy() {
    // Make sure to cleanup SignalR event handlers when removing the component
    this.$gameHub.$off("update-game");
  },
};
</script>

<style scoped>
.home {
  margin-left: 40%;
}
h3 {
  float: left;
  margin: 40px;
}
img {
  height: 100px;
  float: left;
}

.head,
.options {
  width: 400px;
  display: block;
}

a {
  text-align: center;
  text-decoration: none;
  line-height: 80px;
}

.head {
  margin-top: 5%;
  height: 100px;
}

.options {
  margin-top: 10%;
  margin-left: 5%;
}

.button {
  cursor: pointer;
}

.create {
  display: block;
  overflow: hidden;
  text-transform: uppercase;
  border: 1px solid #77da9e;
  color: #77da9e;
  border-radius: 40px;
  background-color: rgba(0, 0, 0, 0.048);
  font-weight: 400;
  width: 250px;
  transition: 0.5s ease-in-out;
}

.create:hover {
  color: #f5f5f5;
  border: 1px solid #f5f5f5;
  background-color: #77da9e;
}
</style>
