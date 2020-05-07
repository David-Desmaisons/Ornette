import vertical from "./volume-vertical";
import horizontal from "./volume-horizontal";

export default {
  functional: true,
  props: {
    vertical: {
      type: Boolean,
      default: false
    },
    value: {
      type: Number,
      required: true
    },
    wheelIncrement: {
      type: Number,
      default: 5
    }
  },
  render: function(h, context) {
    const {
      props: { value, wheelIncrement, vertical: isVertical },
      data: {
        staticClass
      },
      listeners
    } = context;
    const component = isVertical ? vertical : horizontal;
    return h(component, {
      props: {
        value,
        wheelIncrement
      },
      class: staticClass,
      on: listeners
    });
  }
};
