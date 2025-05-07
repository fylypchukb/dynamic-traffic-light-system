"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Switch } from "@/components/ui/switch";
import { configurationApi, trafficLightApi } from "@/lib/api";
import { Configuration, TrafficLight } from "@/lib/api-types";
import { PageLoading } from "@/components/layout/page-loading";
import { useToast } from "@/hooks/use-toast";
import { Plus, Trash } from "lucide-react";
import { Card, CardContent } from "@/components/ui/card";

const formSchema = z.object({
  trafficLightId: z.string().min(1, "Traffic light is required"),
  minGreenTime: z.string()
    .min(1, "Minimum green time is required")
    .refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  maxGreenTime: z.string()
    .min(1, "Maximum green time is required")
    .refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  defaultGreenTime: z.string()
    .min(1, "Default green time is required")
    .refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  defaultRedTime: z.string()
    .min(1, "Default red time is required")
    .refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  timePerVehicle: z.string()
    .min(1, "Time per vehicle is required")
    .refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  isActive: z.boolean(),
  sequenceGreenTime: z.array(z.object({
    cars: z.string().refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
    duration: z.string().refine((val) => !isNaN(Number(val)) && Number(val) > 0, {
      message: "Must be a positive number",
    }),
  })),
});

interface ConfigurationFormProps {
  id: number | null;
}

export function ConfigurationForm({ id }: ConfigurationFormProps) {
  const [isLoading, setIsLoading] = useState(true);
  const [configuration, setConfiguration] = useState<Configuration | null>(null);
  const [trafficLights, setTrafficLights] = useState<TrafficLight[]>([]);
  const router = useRouter();
  const { toast } = useToast();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      trafficLightId: "",
      minGreenTime: "30",
      maxGreenTime: "120",
      defaultGreenTime: "45",
      defaultRedTime: "60",
      timePerVehicle: "2",
      isActive: true,
      sequenceGreenTime: [],
    },
  });

  useEffect(() => {
    async function fetchData() {
      try {
        const [trafficLightsResponse, configurationResponse] = await Promise.all([
          trafficLightApi.getAll(),
          id ? configurationApi.getById(id) : null,
        ]);

        setTrafficLights(trafficLightsResponse.result || []);

        if (configurationResponse) {
          const config = configurationResponse.result;
          setConfiguration(config);
          
          // Convert sequenceGreenTime object to array format
          const sequenceArray = config.sequenceGreenTime ? 
            Object.entries(config.sequenceGreenTime).map(([cars, duration]) => ({
              cars: cars,
              duration: duration?.toString() || "0",
            })) : [];

          form.reset({
            trafficLightId: config.trafficLightId.toString(),
            minGreenTime: config.minGreenTime.toString(),
            maxGreenTime: config.maxGreenTime.toString(),
            defaultGreenTime: config.defaultGreenTime.toString(),
            defaultRedTime: config.defaultRedTime.toString(),
            timePerVehicle: config.timePerVehicle.toString(),
            isActive: config.isActive,
            sequenceGreenTime: sequenceArray,
          });
        }
      } catch (error) {
        console.error("Failed to fetch data:", error);
        toast({
          title: "Error",
          description: "Failed to load required data",
          variant: "destructive",
        });
      } finally {
        setIsLoading(false);
      }
    }

    fetchData();
  }, [id, form, toast]);

  const addSequence = () => {
    const currentSequences = form.getValues("sequenceGreenTime");
    form.setValue("sequenceGreenTime", [
      ...currentSequences,
      { cars: "", duration: "" },
    ]);
  };

  const removeSequence = (index: number) => {
    const currentSequences = form.getValues("sequenceGreenTime");
    form.setValue("sequenceGreenTime", currentSequences.filter((_, i) => i !== index));
  };

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      // Convert sequence array to object format
      const sequenceObject: Record<string, number> = {};
      values.sequenceGreenTime.forEach(({ cars, duration }) => {
        sequenceObject[cars] = parseInt(duration);
      });

      const payload = {
        ...values,
        trafficLightId: parseInt(values.trafficLightId),
        minGreenTime: parseInt(values.minGreenTime),
        maxGreenTime: parseInt(values.maxGreenTime),
        defaultGreenTime: parseInt(values.defaultGreenTime),
        defaultRedTime: parseInt(values.defaultRedTime),
        timePerVehicle: parseInt(values.timePerVehicle),
        sequenceGreenTime: sequenceObject,
      };

      if (id) {
        await configurationApi.update(id, payload);
        toast({
          title: "Success",
          description: "Configuration updated successfully",
        });
      } else {
        await configurationApi.create(payload);
        toast({
          title: "Success",
          description: "Configuration created successfully",
        });
      }
      router.push("/configurations");
    } catch (error) {
      console.error("Failed to save configuration:", error);
      toast({
        title: "Error",
        description: "Failed to save configuration",
        variant: "destructive",
      });
    }
  }

  if (isLoading) {
    return <PageLoading />;
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="trafficLightId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Traffic Light</FormLabel>
              <Select
                onValueChange={field.onChange}
                defaultValue={field.value}
              >
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Select a traffic light" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  {trafficLights.map((light) => (
                    <SelectItem
                      key={light.id}
                      value={light.id.toString()}
                    >
                      {light.name || `Traffic Light #${light.id}`}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <FormDescription>
                The traffic light this configuration applies to
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <div className="grid gap-6 md:grid-cols-2">
          <FormField
            control={form.control}
            name="minGreenTime"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Minimum Green Time (seconds)</FormLabel>
                <FormControl>
                  <Input type="number" min="1" {...field} />
                </FormControl>
                <FormDescription>
                  Minimum duration of green light
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="maxGreenTime"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Maximum Green Time (seconds)</FormLabel>
                <FormControl>
                  <Input type="number" min="1" {...field} />
                </FormControl>
                <FormDescription>
                  Maximum duration of green light
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <div className="grid gap-6 md:grid-cols-2">
          <FormField
            control={form.control}
            name="defaultGreenTime"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Default Green Time (seconds)</FormLabel>
                <FormControl>
                  <Input type="number" min="1" {...field} />
                </FormControl>
                <FormDescription>
                  Default duration of green light
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="defaultRedTime"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Default Red Time (seconds)</FormLabel>
                <FormControl>
                  <Input type="number" min="1" {...field} />
                </FormControl>
                <FormDescription>
                  Default duration of red light
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <FormField
          control={form.control}
          name="timePerVehicle"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Time Per Vehicle (seconds)</FormLabel>
              <FormControl>
                <Input type="number" min="1" step="0.1" {...field} />
              </FormControl>
              <FormDescription>
                Additional green time per vehicle
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <Card>
          <CardContent className="pt-6">
            <div className="flex items-center justify-between mb-4">
              <div>
                <h3 className="text-lg font-medium">Custom Vehicle Count Durations</h3>
                <p className="text-sm text-muted-foreground">
                  Set specific green light durations for different vehicle counts
                </p>
              </div>
              <Button type="button" onClick={addSequence} variant="outline" size="sm">
                <Plus className="h-4 w-4 mr-2" />
                Add Sequence
              </Button>
            </div>

            <div className="space-y-4">
              {form.watch("sequenceGreenTime").map((_, index) => (
                <div key={index} className="flex gap-4 items-start">
                  <FormField
                    control={form.control}
                    name={`sequenceGreenTime.${index}.cars`}
                    render={({ field }) => (
                      <FormItem className="flex-1">
                        <FormLabel>Number of Vehicles</FormLabel>
                        <FormControl>
                          <Input type="number" min="1" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name={`sequenceGreenTime.${index}.duration`}
                    render={({ field }) => (
                      <FormItem className="flex-1">
                        <FormLabel>Duration (seconds)</FormLabel>
                        <FormControl>
                          <Input type="number" min="1" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <Button
                    type="button"
                    variant="ghost"
                    size="icon"
                    className="mt-8"
                    onClick={() => removeSequence(index)}
                  >
                    <Trash className="h-4 w-4" />
                  </Button>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        <FormField
          control={form.control}
          name="isActive"
          render={({ field }) => (
            <FormItem className="flex flex-row items-center justify-between rounded-lg border p-4">
              <div className="space-y-0.5">
                <FormLabel className="text-base">Active Status</FormLabel>
                <FormDescription>
                  Whether this configuration is currently active
                </FormDescription>
              </div>
              <FormControl>
                <Switch
                  checked={field.value}
                  onCheckedChange={field.onChange}
                />
              </FormControl>
            </FormItem>
          )}
        />

        <div className="flex justify-end gap-4">
          <Button
            type="button"
            variant="outline"
            onClick={() => router.push("/configurations")}
          >
            Cancel
          </Button>
          <Button type="submit">
            {id ? "Update Configuration" : "Create Configuration"}
          </Button>
        </div>
      </form>
    </Form>
  );
}