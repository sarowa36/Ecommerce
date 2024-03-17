<script setup>
import { computed } from 'vue';
import { Guid } from 'guid-typescript';

defineProps({
    placeholder: "",
    modelValue: "",
    id: {
        type: String,
        default: null
    },
    errorMessage: {
        type: String,
        default: ""
    },
    type:{
        type:String,
        default:"text"
    }
})
defineEmits(["update:modelValue", "pressEnter"])
</script>
<template>
    <div class="form_group">
        <span :class="{'active':errorMessage}" class="text-danger">{{ errorMessage ? errorMessage :  "&nbps;" }}</span>
        <input :id="_id" ref="textbox" :type="type" :class="'myinput ' + (value ? 'with_value' : '')" :placeholder="placeholder"
            @blur="checkVal" v-model="value" @keydown="keydownEvent" @input="inputEvent" />
        <label :for="_id" class="input_label text_theme">{{ placeholder }}</label>
    </div>
</template> 
<script>
export default {
    data() {
        return {
            _id: ""
        }
    },
    methods: {
        checkVal(e) {
            const node = this.$refs.textbox;
            if (node.value && !node.classList.contains("with_value")) {
                node.classList.add("with_value");
            }
            else if (!node.value && node.classList.contains("with_value")) {
                node.classList.remove("with_value");
            }
        },
        keydownEvent(e) {
            if (e.keyCode == 13) {
                this.$emit("pressEnter")
            }
        },
        inputEvent(){
            const node = this.$refs.textbox;
            if(this.type=="number"){
            node.value=node.value.replace(/[^0-9]/g,'')
        }
            
        }
    },
    mounted() {
        const node = this.$refs.textbox;
        if (this.modelValue) {
            node.value = this.modelValue;
            node.classList.add("with_value")
        }
        if (!this.id)
            this._id = Guid.create().toString()
        else
            this._id = this.id
    },
    computed: {
        value: {
            get() {
                return this.modelValue;
            },
            set(val) {
                this.$emit("update:modelValue", val)
            }
        }
    },
    watch: {
        value(newVal, oldVal) {
            if (!newVal) {
                this.$refs.textbox.classList.remove("with_value");
            }
        }
    }
}
</script>
<style scoped>
.input_label {
    width: fit-content;
    transform: translateY(100%);
    transform-origin: left;
    cursor: text;
    transition: 0.3s;
    z-index: 50;
}

.myinput:focus,
.myinput.with_value {
    border-bottom-color: var(--second-color);
}

.myinput:focus + .input_label,
.input_label::selection,
.myinput.with_value+.input_label {
    transform: scale(85%);
}
input.myinput::-webkit-outer-spin-button,
input.myinput::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
.myinput[type="number"]{
    -moz-appearance: textfield;
}
.myinput {
    background-color: transparent;
    border: 0 var(--first-color) solid;
    border-bottom: 1px solid var(--first-color);
    color: var(--first-color);
    outline: none;
    transition: 0.3s;
    z-index: 100;
}

.form_group {
    display: flex;
    flex-direction: column-reverse;
}

.form_group>span {
    white-space: pre-line;
    opacity: 0;
}
.form_group>span.active{
    opacity: 1;
}
input.myinput::placeholder,
textarea.myinput::placeholder {
    color: transparent;
}

input.myinput:focus,
input.myinput:focus-visible {
    outline: none;
}</style>