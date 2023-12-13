<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <div class="container mt-5 pt-1">
        <div class="row mt-3">
            <div class="col-md-4 p-3">
                <div class="contact_box">
                <FontAwesomeIcon icon="phone" />
                <div class="contact_box_content">
                    <span>+90 532 532 53 32</span>
                    <span>+90 532 532 53 32</span>
                </div>
            </div>
            </div>
            <div class="col-md-4 p-3">
                <div class="contact_box">
                <FontAwesomeIcon icon="envelope" />
                <div class="contact_box_content">
                    <span>info@salsha.com</span>
                </div>
            </div>
            </div>
            <div class="col-md-4 p-3">
                <div class="contact_box">
                <FontAwesomeIcon icon="location-dot" />
                <div class="contact_box_content">
                    <span>Kültür, Atatürk Cd., 35220 Konak/İzmir</span>
                </div>
            </div>
            </div>
        </div>
    </div>
    <div class="container-fluid mt-5">
        <div class="row">
        <div class="col-lg-6 p-0">
            <iframe class="contact_map" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d50013.51144699162!2d27.087961476564416!3d38.42195913382603!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd8e2fece48eb%3A0xafa58b890c33632a!2zS29uYWsvxLB6bWly!5e0!3m2!1str!2str!4v1700420125649!5m2!1str!2str" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
        </div>
        <div class="col-lg-6 contact_form p-5 theme_bg_3">
            <div class="row">
                <h3 class="col-12 mb-3 text-center">Contact Us</h3>
                <div class="col-6 form-group">
                    <label class="form-label">Name</label>
                    <input class="form-control" type="text">
                </div>
                <div class="col-6 form-group">
                    <label class="form-label">Surname</label>
                    <input class="form-control" type="text">
                </div>
                <div class="col-6 form-group">
                    <label class="form-label">Phone</label>
                    <input class="form-control" type="text">
                </div>
                <div class="col-6 form-group">
                    <label class="form-label">Email</label>
                    <input class="form-control" type="text">
                </div>
                <div class="col-12 form-group">
                    <label class="form-label">Subject</label>
                    <input class="form-control" type="text">
                </div>
                <div class="col-12 form-group">
                    <label class="form-label">Message</label>
                    <textarea class="form-control" cols="30" rows="10"></textarea>
                </div>
                <div class="col-12 mt-3 text-end">
                    <button class="btn btn-primary">Send</button>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>
<style>
.contact_box{
    border:1px solid var(--sixth-color);
    padding: 35px 15px;
    display: flex;
    flex-direction: column;
    gap:15px;
    height: 100%;
}
.contact_box>svg{
    font-size: 35px;
}
.contact_box_content{
    display: flex;
    flex-direction: column;
    align-items: center;
}
.contact_map{
    width: 100%;
    height: 100%;
    border: 0;
}
.contact_form .form-label{
    margin-top: .5rem;
}
.contact_form .form-control:where(:focus, :focus-visible, :focus-visible, :target) {
  border-color: var(--fourth-color);
  outline: 0;
  box-shadow: 0 0 3px .2rem color-mix(in srgb, var(--second-color) 50%, transparent);
}
@media (max-width:992px) {
    .contact_map{
        height: 400px;;
    }
}
</style>