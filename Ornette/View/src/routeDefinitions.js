import main from "./pages/main";
import about from "./pages/about";
import demo from "./pages/demo";

import { isDesignTime } from "@/infra/environment";

const routeDefinitions = [
  { name: "main", component: main, menu: { icon: "fa-television" } },
  { name: "about", component: about, menu: { icon: "info" } }
];
if (isDesignTime()) {
  routeDefinitions.splice(1, 0, {
    name: "test",
    component: demo,
    menu: { icon: "mdi-test-tube" }
  });
}

export default routeDefinitions;
