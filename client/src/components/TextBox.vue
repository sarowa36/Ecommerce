<script setup>
import { computed } from 'vue';
import { Guid } from 'guid-typescript';

defineProps({
    type:{
        type:String,
        default:"textbox"
    },
    placeholder:"",
    modelValue:"",
    id:Guid.create().toString(),
    whenPressEnter:{
        type:Function,
        default:()=>{}
    }
})
defineEmits(["update:modelValue"])
</script>
<template>
    <div class="form_group">
        <span v-if="!error.isValid"></span>
        <input :id="id" ref="textbox" :class="'myinput '+ (value ? 'with_value':'')" :placeholder="placeholder" @blur="checkVal" v-model="value" @keydown="keydownEvent" />
        <label :for="id" class="input_label">{{placeholder}}</label>
    </div>
</template> 
<script>
export default {
    data(){
        return{
        error:{
            isValid:true,
            list:[]
        }
        };
    },
    methods:{
        checkVal(e){
           const node=this.$refs.textbox;
           if(node.value && !node.classList.contains("with_value")){
            node.classList.add("with_value");
           }
           else if(!node.value && node.classList.contains("with_value"))
           {
            node.classList.remove("with_value");
           }
        },
        keydownEvent(e){
            if(e.keyCode==13){
                this.whenPressEnter()
            }
        }
    },
    mounted() {
     const node=this.$refs.textbox;
        if(this.modelValue && this.type!="file"){
            node.value=this.modelValue;
            node.classList.add("with_value")
        }
    },
    computed:{
      value:{
        get(){
            return this.modelValue;
        },
        set(val){
            this.$emit("update:modelValue", val)
        }
      }  
    },
    watch:{
        value(newVal, oldVal){
            if(!newVal){
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
    cursor: text;
    transition: 0.3s;
}

.myinput:focus, .myinput.with_value {
    border-bottom-color: var(--second-color);
}

.myinput:focus+.input_label, .myinput.with_value + .input_label {
    transform: scale(85%);
}

.myinput {
    background-color: transparent;
    border: 0 var(--first-color) solid;
    border-bottom: 1px solid var(--first-color);
    color: var(--first-color);
    outline: none;
    transition: 0.3s;
}

.form_group {
    display: flex;
    flex-direction: column-reverse;
}

input.myinput::placeholder, textarea.myinput::placeholder {
    color: transparent;
}

input.myinput:focus,
input.myinput:focus-visible {
    outline: none;
}
</style>