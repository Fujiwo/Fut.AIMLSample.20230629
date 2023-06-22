#coding: utf-8

import RPi.GPIO as GPIO
import time

gpio_number = 4

GPIO.setmode(GPIO.BCM)
GPIO.setup(gpio_number, GPIO.OUT)
for x in xrange(5):
    GPIO.output(gpio_number, True)
    time.sleep(2)
    GPIO.output(gpio_number, False)
    time.sleep(2)
GPIO.cleanup()
