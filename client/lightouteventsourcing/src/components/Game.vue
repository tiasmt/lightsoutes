<template>
  <div class="game">
    <div class="head">
      <img
        alt="Lights On"
        src="../assets/images/ON.svg"
        class="image__header"
      />
      <img
        alt="Lights Off"
        src="../assets/images/OFF.svg"
        class="image__header"
      />
    </div>
    <div class="info__area">
      <div class="info">
        <label class="gamename__label" for="gamename">Game Name </label
        ><span class="gamename__text">{{ gameName }}</span>
      </div>
      <br />
      <div class="info">
        <label class="boardsize__label" for="boardsize">Board Size </label
        ><span class="boardsize__text">{{ boardSize }}</span>
      </div>
    </div>
    <div class="board__area">
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
    <div class="events__area">
      <label class="events__label" >Events</label>
      <div class="events__all">
        <li v-for="(event, index) in events" :key="index" class="event">
          {{ event }}
        </li>
      </div>
    </div>
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
      this.events = [];
      this.$store.dispatch("ToggleLight", {
        x: x,
        y: y,
      });
    },
  },
  mounted() {
    var that = this;
    this.$gameHub.$on("update-game", (data) => {
      this.$store.dispatch("Update", data);
    });
    this.$gameHub.$on("send-event", (data) => {
      that.events.push(data);
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
    height: 90vh;
}
h3 {
  float: left;
  margin: 40px;
}


.image__header {
  height: 100px;
  float: left;
  margin-right: 5%;
}

.head {
  display: block;
  margin-left: 40%;
}

a {
  text-align: center;
  text-decoration: none;
  line-height: 80px;
}

.head {
  /* padding-top: 5%; */
  height: 100px;
}

.info__area {
  margin: 8% 0%;
  font-size: 70%;
  width: 50%;
  /* color: black; */
}

.info-area label {
  font-weight: 600;
  margin-top: 5px;
}


label {
  text-transform: uppercase;
  border-radius: 40px;
  background-color: rgba(0, 0, 0, 0.048);
  width: 250px;
  padding: 5px;
}

.gamename__label {
  color: #77a2da;
  border: 1px solid #77a2da;
}

.events__label {
  color: #77da9e;
  border: 1px solid #77da9e;
  font-size: 80%;
  padding: 1% 4%;
}

.boardsize__label {
  color: #da7777;
  border: 1px solid #da7777;
}
.gamename__text,
.boardsize__text {
  margin: 1%;
  text-transform: uppercase;
}
.board__area {
  padding: 10% 10%;
  float: left;
  background-color: rgba(0, 0, 0, 0.219);
  border-radius: 50%;
  display:flex;
  /* border: 1px solid rgba(255, 255, 255, 0.089); */
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

.events__area {
  width: 50%;
  float: right;
  padding-left: 2%;
  margin-top: -4%;
  height: 60vh;
  border-left: 1px solid #77da9f5d;;
}

.events__area h2 {
  font-size: 75%;
}

.events__all {
  margin-top: 4%;
}

.event {
  margin: 1%;
  font-size: 40%;
  list-style: none;
}
</style>
