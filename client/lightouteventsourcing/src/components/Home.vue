<template>
  <div class="home">
    <div class="head">
      <img alt="Lights On" src="../assets/images/ON.svg" />
      <h3>VS</h3>
      <img alt="Lights Off" src="../assets/images/OFF.svg" />
    </div>
    <div class="summary">
      Lights Out is a puzzle game consisting of an n x n grid of lights. At the
      beginning of the game, some of the lights are switched on. When a light is
      pressed, this light and the four adjacent lights are toggled, i.e., they
      are switched on if they were off, and switched off otherwise. The purpose
      of the game is to switch all the lights off.
    </div>
    <div class="options">
      <input class="gamename" v-model="gameName" placeholder="Game Name" />
      <input class="boardsize" v-model="boardSize" placeholder="Board Size" />
      <a class="button create" @click="CreateGame()">New Game</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
  data() {
    return {
      gameName: "",
      boardSize: "",
    };
  },
  methods: {
    CreateGame() {
      this.$store.dispatch("CreateGame", {
        gameName: this.gameName,
        boardSize: this.boardSize,
      });
    },
  },
  created() {
    this.$gameHub.$on("update-game", (data) => {
      this.$store.dispatch("Update", data);
    });
    this.$gameHub.$on("connected", (data) => {
      this.$store.dispatch("Connected", data);
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
  margin-top: 5%;
  margin-left: 5%;
}

.button {
  cursor: pointer;
}

.summary {
  margin-top: 5%;
  font-size: 50%;
  margin-left: 0%;
  width: 60%;
}

.create,
.gamename,
.boardsize {
  display: block;
  overflow: hidden;
  text-transform: uppercase;
  border-radius: 40px;
  background-color: rgba(0, 0, 0, 0.048);
  font-weight: 400;
  width: 250px;
  transition: 0.4s ease-in-out;
  margin-top: 5%;
}

.gamename {
  color: #77a2da;
  border: 1px solid #77a2da;
}

.boardsize {
  color: #da7777;
  border: 1px solid #da7777;
}

.create {
  color: #77da9e;
  border: 1px solid #77da9e;
}

.create:hover {
  color: rgb(92, 90, 90);
  border: 1px solid rgb(92, 90, 90);
  background-color: #77da9e;
}

.gamename,
.boardsize {
  font-size: 14px;
  line-height: 30px;
  text-align: center;
  float: left;
  width: 120px;
  margin-bottom: 5%;
  margin-right: 5px;
  font-weight: 700;
}

.boardsize::placeholder {
  color: #da7777;
}

.gamename::placeholder {
  color: #77a2da;
}
</style>
