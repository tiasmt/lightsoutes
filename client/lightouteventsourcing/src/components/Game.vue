<template>
  <div class="game">
    <div class="head">
      <img
        alt="Lights On"
        src="../assets/images/ON.svg"
        class="image__header"
      />
      <h3>VS</h3>
      <img
        alt="Lights Off"
        src="../assets/images/OFF.svg"
        class="image__header"
      />
    </div>
    <div class="board">
      <div v-for="i in boardSize" :key="i" class="row">
        <div v-for="j in boardSize" :key="j">
          <img
            v-if="
              lightsOn.find((item) => item.y == i - 1 && item.x == j - 1) !=
              undefined
            "
            alt="Lights On"
            src="../assets/images/ON.svg"
            class="light"
            @click="ToggleLight(j - 1, i - 1)"
          />
          <img
            v-else
            alt="Lights Off"
            src="../assets/images/OFF.svg"
            class="light"
            @click="ToggleLight(j - 1, i - 1)"
          />
        </div>
        <br />
      </div>
    </div>
    <li v-for="(event, index) in events" :key="index" class="events">{{ event }}</li>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "Game",
  data() {
    return {
      events: [],
    };
  },
  computed: {
    ...mapState(["gameName", "boardSize", "lightsOn", "isActive"]),
  },
  methods: {
    ToggleLight(x, y) {
      this.$store.dispatch("ToggleLight", {
        gameName: "test",
        x: x,
        y: y,
      });
    },
  },
  created() {
    var that = this;
    this.$gameHub.$on("update-game", (data) => {
      that.boardSize = data.boardSize;
      that.lightsOn = data.lightsOn.on;
      that.isActive = data.isActive;
      console.log(that.lightsOn);
    });
    this.$gameHub.$on("send-event", (data) => {
      that.events.push(data);
      console.log(data);
    });
  },
  beforeDestroy() {
    // Make sure to cleanup SignalR event handlers when removing the component
    this.$gameHub.$off("update-game");
    this.$gameHub.$off("send-event");
  },
};
</script>

<style scoped>
.game {
  margin-left: 40%;
}
h3 {
  float: left;
  margin: 40px;
}
.image__header {
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

.board {
  margin-top: 10%;
  margin-left: 28%;
}

.row {
  height: 55px;
}

.button,
.light {
  cursor: pointer;
  border: 1px solid #47494841;
  border-radius: 5px;
}

.light {
  height: 50px;
  float: left;
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

.events {
  margin-top: 5%;
  font-size: 50%;
}
</style>
