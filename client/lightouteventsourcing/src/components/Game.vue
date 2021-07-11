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
      <div class="slidecontainer">
        <input
          type="range"
          min="1"
          :max="moveIndex"
          :value="moveNumber"
          class="slider"
          id="myRange"
          @input="UpdateMove"
          @mouseup="ReplayMove"
        />
        <p>Move: {{ moveNumber }}</p>
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
      <label class="events__label">Events</label>
      <div class="events__all">
        <li v-for="(event, index) in events" :key="index" class="event">
          <label class="events__label mini">{{ event.eventType }}</label>
          <label class="events__label mini position"
            >X: {{ event.posX }}, Y:{{ event.posY }}</label
          >
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
      moveNumber: 1,
      moveIndex: 1,
    };
  },
  computed: {
    ...mapState(["gameName", "boardSize", "lightsOn", "isActive"]),
  },
  methods: {
    ToggleLight(x, y) {
      this.moveIndex++;
      this.moveNumber = this.moveIndex;
      this.events = [];
      this.$store.dispatch("ToggleLight", {
        x: x,
        y: y,
      });
    },
    UpdateMove() {
      this.events = [];
      var slider = document.getElementById("myRange");
      this.moveNumber = slider.value;
    },
    ReplayMove() {
      this.$store.dispatch("Replay", this.moveNumber);
    },
  },
  mounted() {
    var that = this;
    this.$gameHub.$on("update-game", (data) => {
      this.$store.dispatch("Update", data);
    });
    this.$gameHub.$on("send-event", (data) => {
      var event = {};
      event.eventType = data.eventType;
      if (data.eventType == "LightToggled") {
        event.posX = data.posX;
        event.posY = data.posY;
      }

      that.events.push(event);
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
  height: 100px;
}

.info__area {
  margin: 5% 0%;
  font-size: 70%;
  width: 55%;
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

.mini {
  padding-top: 3px;
  padding-bottom: 3px;
  padding-left: 10px;
  margin-left: 2%;
  margin-top: -15px;
  margin-right: 3px;
  width: 8%;
  float: left;
  background-color: #77da9e;
  color: rgb(92, 90, 90);
  font-weight: 800;
}

.position {
  padding-right: 0px;
  margin-top: -15px;
  background-color: #f8cc3a;
  border: 1px solid #f8cc3a;
  font-size: 90%;
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
  padding: 5% 5%;
  margin-left: 10%;
  float: left;
  background-color: rgba(0, 0, 0, 0.219);
  border-radius: 50%;
  display: flex;
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
  border-left: 1px solid #77da9f5d;
}

.events__area h2 {
  font-size: 75%;
}

.events__all {
  margin-top: 8%;
}

.event {
  margin-bottom: 3%;
  font-size: 35%;
  list-style: none;
}

.event__text {
  margin-left: 10px;
  margin-top: -10px;
}

li:before {
  content: " ";
  background-color: #77da9e;
  height: 10px;
  width: 10px;
  border-radius: 50%;
  display: block;
  margin-left: -4px;
  margin-top: -3%;
  position: relative;
}
li:after {
  content: " ";
  background-color: #77da9e;
  width: 1px;
  padding: 15px 1px;
  display: block;
  margin-top: -1%;
  position: relative;
}

.slidecontainer {
  width: 25%;
  text-transform: uppercase;
  float: right;
  margin: -10% 0% 10% 0%;
}

.slider {
  -webkit-appearance: none;
  width: 100%;
  height: 5px;
  background: #d3d3d360;
  outline: none;
  opacity: 0.5;
  -webkit-transition: 0.2s;
  transition: opacity 0.2s;
}

.slider:hover {
  opacity: 1;
}

.slider::-webkit-slider-thumb {
  -webkit-appearance: none;
  appearance: none;
  width: 25px;
  height: 25px;
  border-radius: 50%;
  background: #f8cc3a;
  cursor: pointer;
}
</style>
