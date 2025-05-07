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
import { intersectionApi, trafficLightApi } from "@/lib/api";
import { Intersection, TrafficLight } from "@/lib/api-types";
import { PageLoading } from "@/components/layout/page-loading";
import { useToast } from "@/hooks/use-toast";

const formSchema = z.object({
  name: z.string().min(1, "Name is required"),
  intersectionId: z.string().min(1, "Intersection is required"),
  priority: z.string().min(1, "Priority is required"),
  isActive: z.boolean(),
});

interface TrafficLightFormProps {
  id: number | null;
}

export function TrafficLightForm({ id }: TrafficLightFormProps) {
  const [isLoading, setIsLoading] = useState(true);
  const [trafficLight, setTrafficLight] = useState<TrafficLight | null>(null);
  const [intersections, setIntersections] = useState<Intersection[]>([]);
  const router = useRouter();
  const { toast } = useToast();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      intersectionId: "",
      priority: "1",
      isActive: true,
    },
  });

  useEffect(() => {
    async function fetchData() {
      try {
        const [intersectionsResponse, trafficLightResponse] = await Promise.all([
          intersectionApi.getAll(),
          id ? trafficLightApi.getById(id) : null,
        ]);

        setIntersections(intersectionsResponse.result || []);

        if (trafficLightResponse) {
          const light = trafficLightResponse.result;
          setTrafficLight(light);
          form.reset({
            name: light.name || "",
            intersectionId: light.intersectionId.toString(),
            priority: light.priority.toString(),
            isActive: light.isActive,
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

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      const payload = {
        ...values,
        intersectionId: parseInt(values.intersectionId),
        priority: parseInt(values.priority),
      };

      if (id) {
        await trafficLightApi.update(id, payload);
        toast({
          title: "Success",
          description: "Traffic light updated successfully",
        });
      } else {
        await trafficLightApi.create(payload);
        toast({
          title: "Success",
          description: "Traffic light created successfully",
        });
      }
      router.push("/traffic-lights");
    } catch (error) {
      console.error("Failed to save traffic light:", error);
      toast({
        title: "Error",
        description: "Failed to save traffic light",
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
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Name</FormLabel>
              <FormControl>
                <Input placeholder="Enter traffic light name" {...field} />
              </FormControl>
              <FormDescription>
                A descriptive name for this traffic light
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="intersectionId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Intersection</FormLabel>
              <Select
                onValueChange={field.onChange}
                defaultValue={field.value}
              >
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Select an intersection" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  {intersections.map((intersection) => (
                    <SelectItem
                      key={intersection.id}
                      value={intersection.id.toString()}
                    >
                      {intersection.city} - {intersection.location}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <FormDescription>
                The intersection where this traffic light is located
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="priority"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Priority</FormLabel>
              <Select
                onValueChange={field.onChange}
                defaultValue={field.value}
              >
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Select priority level" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  {[1, 2, 3, 4].map((priority) => (
                    <SelectItem
                      key={priority}
                      value={priority.toString()}
                    >
                      Priority {priority}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <FormDescription>
                The priority level of this traffic light in the intersection
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="isActive"
          render={({ field }) => (
            <FormItem className="flex flex-row items-center justify-between rounded-lg border p-4">
              <div className="space-y-0.5">
                <FormLabel className="text-base">Active Status</FormLabel>
                <FormDescription>
                  Whether this traffic light is currently active
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
            onClick={() => router.push("/traffic-lights")}
          >
            Cancel
          </Button>
          <Button type="submit">
            {id ? "Update Traffic Light" : "Create Traffic Light"}
          </Button>
        </div>
      </form>
    </Form>
  );
}