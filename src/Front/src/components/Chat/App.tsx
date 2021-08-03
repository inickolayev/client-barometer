import { createUseStyles } from "react-jss";
import { Chat } from "./Chat";

function App() {
  const classes = createStyles();

  return (
    <div className={classes.mainContainer}>
      {<Chat chatState={{ room_id: "123", username: "some" }} />}
    </div>
  );
}

export default App;

const createStyles = createUseStyles({
  mainContainer: {
    width: "100%",
    marginLeft: "auto",
    marginRight: "auto",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center"
  },
});