<script setup>
import draggable from "vuedraggable"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import VueEasyLightbox from 'vue-easy-lightbox'
import { FileUploadValue,FileUploadValueArray, ListedFileComponent } from "./";
defineProps({
    modelValue:{
        type:FileUploadValueArray,
        default:[]
    }
})
defineEmits(["update:modelValue"])
</script>
<template>
    <div class="row">
        <div :class="[value.length < 1 ? 'align-items-center d-flex justify-content-center' : '', 'col-7']">
            <draggable v-model="value" handle=".file_move" item-key="id">
                <template #item="{ element, index }">
                    <ListedFileComponent v-model="value[index]" @remove="remove(index)" @showLightbox="showLightbox" />
                </template>
            </draggable>
            <h4 v-if="value.length < 1">There is no file selected</h4>
        </div>
        <div class="col-5">
            <div class="file_drop" @drop="logitems" @dragend="prevent" @dragleave="prevent" @dragover="prevent">
                <FontAwesomeIcon icon="cloud-arrow-up" />
                <h4 class="m-0">Drag & Drop</h4>
                <span>or <span class="text-primary cursor_pointer" @click="$refs.file_temp_input.click()">browse</span> your
                    files</span>
                <input class="d-none" ref="file_storage_input" type="file">
                <input class="d-none" ref="file_temp_input" type="file" multiple @change="fileInputChange">
            </div>
        </div>
    </div>
    <vue-easy-lightbox :visible="lightboxToggler" :imgs="link" @hide="lightboxToggler = false" />
</template>
<style>
.file_drop {
    width: 100%;
    aspect-ratio: 1;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    border: 1px dashed;
    border-radius: 5px;
    gap: 10px;
    background-color: color-mix(in srgb, var(--bs-secondary) 10%, transparent);
}

.file_drop>svg {
    width: 100%;
    height: 50px;
}
</style>
<script>
export default {
    data() {
        return {
            link: "",
            lightboxToggler: false
        }
    },
    methods: {
        logitems(e) {
            e.preventDefault();
            e.stopPropagation();
            var dt = new DataTransfer();
            [...e.dataTransfer.files].forEach(x => this.modelValue.push(new FileUploadValue({file:x})));
            return false;
        },
        fileInputChange() {
            [...this.$refs.file_temp_input.files].forEach(x => this.modelValue.push(new FileUploadValue({file:x})));
        },
        remove(index) {
            this.value=this.modelValue.filter((x, i) => { if (i != index) return x; })
        },
        async showLightbox(item) {
            if (!item.link) {
                var fileReader = new FileReader();
                fileReader.onloadend = (x) => {
                    item.link=fileReader.result;
                    this.link = fileReader.result;
                    this.lightboxToggler = true;
                }
                await fileReader.readAsDataURL(item.file);
            }
            else {
                this.link = item.link;
                this.lightboxToggler = true;
            }
        },
        prevent(e) {
            e.preventDefault();
            e.stopPropagation();
        }
    },
    computed:{
        value:{
            get(){
                return this.modelValue;
            },
            set(val){
                this.$emit("update:modelValue",val)
            }
        }
    }
}
</script>