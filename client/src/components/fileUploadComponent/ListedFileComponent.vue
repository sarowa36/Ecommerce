<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { FileUploadValue } from "./";
defineProps({
    modelValue: {
        type: FileUploadValue,
        required: true
    }
})
defineEmits(["remove", "showLightbox"])
</script>
<template>
    <div class="uploaded_file">
        <font-awesome-icon v-if="!modelValue.link" class="file_icon" icon="image" />
        <img v-else :src="modelValue.link" class="file_icon" alt="">
        <div class="file_meta">{{ modelValue.file !=null ? modelValue.file.name : modelValue.link.split("/").pop() }}<div class="file_size">{{ modelValue.file ? ~~(modelValue.file.size / 1024) +"kb" :'' }}
            </div>
        </div>
        <div class="file_buttons">
            <button class="btn file_show" @click='$emit("showLightbox", modelValue)'>
                <FontAwesomeIcon icon="eye" />
            </button>
            <button class="btn file_move">
                <FontAwesomeIcon icon="arrows-up-down-left-right" />
            </button>
            <button class="btn file_remove" @click="$emit('remove')">
                <FontAwesomeIcon icon="close" />
            </button>
        </div>
    </div>
</template>
<style scoped>
.uploaded_file {
    display: flex;
    align-items: center;
    gap: 15px;
    padding: 15px;
}

.uploaded_file:not(:first-child) {
    border-top: 1px solid color-mix(in srgb, var(--first-color) 30%, transparent);
}

.file_icon {
    width: 50px;
    height: 100%;
}

.file_meta {
    width: 150px;
    font-size: 15px;
    word-break: break-all;
}

.file_buttons {
    display: flex;
    justify-content: end;
    gap: 10px;
    flex-grow: 1;
}

.file_buttons :where(.btn, button) {
    pointer-events: all;
}

.file_buttons>button {
    width: 45px;
    height: 45px;
}

.file_size {
    opacity: 0.7;
}
</style>